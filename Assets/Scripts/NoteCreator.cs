using System;
using UnityEngine;


public class NoteCreator : MonoBehaviour
{

    public SequenceManager SequenceManager;

    public AudioSource AudioSource;
    
    public NotePair green;
    public NotePair red;
    public NotePair blue;
    public NotePair yellow;

    public SpriteRenderer spr;

    [Serializable]
    public struct NotePair
    {
        public InputManager Manager;
        public NoteMover prefab;
        
    }
    
    public float tempo;

    public int preview;

    public float secPerBeat;

    public float songPosition;
    
    public float beatTime;

    public float nextBeat = 0f;
    public float lastBeatTime;
    public float nextBeatTime;

    private float difference;

    public static int machin;


    private void Start()
    {
        SequenceManager.LoadFile();
        secPerBeat = 60f / tempo;
        AudioSource.Play();
    }

    public void Update()
    {
        songPosition = AudioSource.time;
        if ((songPosition - Time.deltaTime) >= nextBeatTime)
        {
            beatTime++;
            nextBeatTime = secPerBeat * beatTime;
            //InstNotes();
            spr.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
    }

    private void InstNotes()
    {
        if (SequenceManager.sequences.Count == 0) return;
        var current = SequenceManager.sequences.Dequeue();
        
        if(current.Left) InstNote(green);
        if(current.Down) InstNote(red);
        if(current.Up) InstNote(yellow);
        if(current.Right) InstNote(blue);
    }

    private void InstNote(NotePair note)
    {
        var nt = Instantiate(note.prefab);
        nt.movespeed = NoteMover.goal / (secPerBeat * preview);
        nt.Manager = note.Manager;
    }
}