using System;
using System.Collections;
using UnityEngine;


public class NoteCreator : MonoBehaviour
{

    public SequenceManager SequenceManager;

    public NotePair green;
    public NotePair red;
    public NotePair blue;
    public NotePair yellow;

    public GameObject noteParent;

    public SpriteRenderer spr;

    public ScoreManager scr;

    [Serializable]
    public struct NotePair
    {
        public InputManager Manager;
        public NoteMover prefab;
    }
    
    public int preview;

    public float secPerBeat;

    private void Start()
    {
        goal = green.Manager.transform.position.x - noteParent.transform.position.x;
        GetComponent<BeatObserver>().WhenBeat += InstNotes;
        SequenceManager.LoadFile();
        // secPerBeat = 60f / tempo;
        // double initTime = AudioSettings.dspTime;
        // AudioSource.PlayScheduled(initTime);
    }

    public void Update()
    {
        // songPosition = AudioSource.time;
        // if ((songPosition - Time.deltaTime) >= nextBeatTime)
        // {
        //     beatTime++;
        //     nextBeatTime = secPerBeat * beatTime;
        //     InstNotes();
        // }
    }
    

    public void InstNotes()
    {
        if (SequenceManager.sequences.Count == 0) return;
        var current = SequenceManager.sequences.Dequeue();
        
        if(current.Left) InstNote(green);
        if(current.Down) InstNote(red);
        if(current.Up) InstNote(yellow);
        if(current.Right) InstNote(blue);
    }

    private bool colorw = false;

    public GameObject colorIndicator;
    
    public static float goal = -7.34F;
    private void InstNote(NotePair note)
    {
        colorIndicator.GetComponent<SpriteRenderer>().color = colorw ? Color.black : Color.white;
        colorw = !colorw;
        var nt = Instantiate(note.prefab,noteParent.transform);
        nt.movespeed = goal / (secPerBeat * preview);
        nt.Manager = note.Manager;
        Vector3 pos = new Vector3(noteParent.transform.position.x,note.Manager.transform.position.y);
        nt.transform.position = pos;
    }
}