using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public Queue<NoteMover> nextNote = new Queue<NoteMover>();

    public KeyCode watchFor;

    private static float perfect = 0.05f;
    private static float good = 0.15f;
    private static float bad = 0.25f;

    private List<float> values = new List<float>();

    public float average;
    
    private void Update()
    {
        if (nextNote.Count == 0) return;
        float notAbs = transform.position.x - nextNote.Peek().transform.position.x;
        float distance = Math.Abs(notAbs);
        //Get distance!
        bool triggered = false;
        if (Input.GetKeyDown(watchFor))
        {
            if (distance > 0.5 && notAbs < 0)
            {
                return;
            }

            triggered = true;
            var n = nextNote.Dequeue();
            Destroy(n.gameObject);
            Debug.Log("got it");
            
        }

        if (Math.Abs(distance) < 0.0001)
        {
            nextNote.Peek().gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (distance > 0.2 && notAbs > 0)
        {
            Debug.Log("missed");
            if(triggered) return;
            Destroy(nextNote.Dequeue().gameObject);
        }
        
    }
}