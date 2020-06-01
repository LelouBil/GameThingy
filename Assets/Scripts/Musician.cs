using System;
using SynchronizerData;
using UnityEngine;

public class Musician : MonoBehaviour
{

    public InputManager manager;

    public Animator animator;

    public BeatObserver observer;
    
    private static readonly int Start1 = Animator.StringToHash("Start");
    private static readonly int Next = Animator.StringToHash("Next");

    public void Start()
    {
        manager.musician = this;
        observer = GetComponentInParent<BeatObserver>();
    }

    public void RunGame()
    {
        animator.SetTrigger(Start1);
        Debug.Log("trigger");
    }

    public void AdvanceAnim()
    {
        animator.SetTrigger(Next);
    }

    public void Update()
    {
        if ((observer.beatMask & BeatType.OnBeat) != 0)
        {
            AdvanceAnim();
            Debug.Log("beat");
        }
    }

    public void Jump()
    {
        //todo
    }
}