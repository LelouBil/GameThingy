using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Queue<NoteMover> nextNote = new Queue<NoteMover>();

    public KeyCode watchFor;

    private float beatTime;

    public GameManager gameManager;

    private static float perfect = 0.05f;
    private static float good = 0.15f;
    private static float bad = 0.25f;

    private List<float> values = new List<float>();

    public float average;
    public Musician musician;

    private void Update()
    {
        if (nextNote.Count == 0) return;
        float notAbs = transform.position.x - nextNote.Peek().transform.position.x;
        float distance = Math.Abs(notAbs);
        //Get distance!
        bool triggered = false;
        if (Math.Abs(distance) < 0.001)
        {
            beatTime = Time.time;
        }
        if (Input.GetKeyDown(watchFor))
        {
            if (distance > 0.2 && notAbs < 0)
            {
                return;
            }

            triggered = true;
            gameManager.scoreManager.UpdateScore(Time.time - beatTime);
            var n = nextNote.Dequeue();
            Destroy(n.gameObject);
        }

        if (Input.GetKey(watchFor))
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
            if (musician != null)
            {
                musician.Jump();
            }
        }
        else{
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        if (Time.time - beatTime > 0.5 && notAbs > 0)
        {
            Debug.Log("missed");
            gameManager.scoreManager.UpdateScore(Time.time - beatTime);
            if(triggered) return;
            Destroy(nextNote.Dequeue().gameObject);
        }
        
    }
}