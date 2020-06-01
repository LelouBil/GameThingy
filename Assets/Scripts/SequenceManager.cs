using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public TextAsset sequenceFile;

    public Queue<Sequence> sequences = new Queue<Sequence>();

    public void LoadFile()
    {
        foreach (string s in sequenceFile.text.Split('\n'))
        {
            if(s != "" && s != "\r" && !s.StartsWith("/")) sequences.Enqueue(ParseSequence(s));
        }
    }

    private Sequence ParseSequence(string s)
    {
        return new Sequence(charToBool(s[0]),charToBool(s[1]),charToBool(s[2]),charToBool(s[3]));
    }

    private bool charToBool(char c)
    {
        return c == '1';
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
