using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SynchronizerData;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public TextAsset sequenceFile;

    public Queue<Sequence> sequences = new Queue<Sequence>();

    
    public List<Measure> measures = new List<Measure>();

    public struct Measure
    {
        public List<Sequence> notes;

        public int beatValue;

        public List<Sequence> toSixteenth()
        {
            int toAdd = (16 - notes.Count) / notes.Count;
            
            List<Sequence> newSeq = new List<Sequence>();
            
            foreach (var note in notes)
            {
                newSeq.Add(note);

                for (int i = 0; i < toAdd; i++)
                {
                    newSeq.Add(new Sequence(false,false,false,false));
                }
            }

            return newSeq;
        }

    }

    public void LoadFile()
    {
        foreach (var measureText in sequenceFile.text.Replace("\r\n","\n").Split(','))
        {
            Debug.Log(measureText);
            measures.Add(ParseMeasure(measureText));
        }

        foreach (var measure in measures)
        {
            foreach (var sequence in measure.toSixteenth())
            {
                sequences.Enqueue(sequence);
            }
        }
    }

    private Measure ParseMeasure(string measureText)
    {
        Measure m = new Measure();
        List<Sequence> notes = new List<Sequence>();
        int counter = 0;
        
        foreach (var s in measureText.Split('\n'))
        {
            if(!Regex.IsMatch(s,@"^\d\d\d\d$")) continue;
            
            notes.Add(ParseSequence(s));

            counter++;
        }

        int beatValue = counter / 4;
        m.beatValue = beatValue > 16 ? 16 : beatValue;
        m.notes = notes;
        return m;
    }

    private Sequence ParseSequence(string s)
    {
        return new Sequence(charToBool(s[0]),charToBool(s[1]),charToBool(s[2]),charToBool(s[3]));
    }

    private bool charToBool(char c)
    {
        return c != '0';
    }

    public struct Sequence
    {
        public Sequence(bool left, bool down, bool up, bool right)
        {
            Up = up;
            Right = right;
            Left = left;
            Down = down;
        }

        public bool Left { get; }
        
        public bool Up { get; }
        
        public bool Right { get; }
        
        public bool Down { get; }
    }
    
}
