using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public TextAsset sequenceFile;

    public string textFile;

    public Queue<Sequence> sequences = new Queue<Sequence>();

    public void LoadFile()
    {
        if (false)
        {
            textFile = new StreamReader((System.IO.Directory.GetCurrentDirectory()) + "/SequenceFile.txt").ReadToEnd();

        }
        else
        {
            textFile = sequenceFile.text;
        }

        foreach (string s in textFile.Split('\n'))
        {
            if (s != "" && s != "\r" && !s.StartsWith("/"))
            {
                var seq = ParseSequence(s);
                int repeat = 1;
                if (s.Contains("x"))
                {
                    repeat = Convert.ToInt32(s.Split('x')[1]);
                }
                
                for (int i = 0; i < repeat; i++)
                {
                    sequences.Enqueue(ParseSequence(s));
                }
                
            }
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
