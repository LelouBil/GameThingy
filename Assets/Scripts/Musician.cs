using System;
using System.Collections.Generic;
using System.Linq;
using SynchronizerData;
using UnityEngine;

public class Musician : MonoBehaviour
{

    public InputManager manager;

    public Animator animator;

    public BeatObserver observer;
    private static readonly int Foot = Animator.StringToHash("foot");

    public Sprite down;

    public NotesData notesData;

    public SpriteRenderer notesRenderer;

    public Sprite up;

    public Sprite cd;

    public Sprite cu;

    public SpriteRenderer spr;

    private bool isdown = true;
    
    private int counter = 0;

    public int missCounter = -1;

    public int noteCounter = -1;

    public List<Sprite> currentNote = new List<Sprite>();


    public int[] data = new int[] {};
    
    

    public void Start()
    {
        notesData = GetComponentInChildren<NotesData>();
        data = new[] {0, 0, 0, 0, 1, 1, 1, 1};
        manager.musician = this;
        observer = GetComponentInParent<BeatObserver>();
        observer.WhenBeat += AdvanceAnim;
    }

    public void RunGame()
    {
        Debug.Log("trigger");
    }

    public void AdvanceAnim()
    {
        counter++;
        if (counter >= data.Length)
        {
            counter = 0;
            down = cd;
            up = cu;
            //todo sfx
        }

        if (missCounter >= 0)
        {
            missCounter++;
        }

        if (missCounter >= 4)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            missCounter = -1;
        }

        if (noteCounter >= 4)
        {
            currentNote = new List<Sprite>();
            notesRenderer.sprite = null;
            noteCounter = -1;
        }
        
        if (noteCounter >= 0)
        {
            if (currentNote.Count >= 1)
            {
                if (noteCounter <= 2)
                {
                    notesRenderer.sprite = currentNote.First();
                }
                else
                {
                    notesRenderer.sprite = currentNote.Last();
                }
            }
            noteCounter++;
        }

        
        
        spr.sprite = data[counter] == 1 ? up : down;
    }
    

    public void Jump()
    {
        //todo
    }
}