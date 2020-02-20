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

        if (Input.GetKeyDown(watchFor))
        {
            values.Add(notAbs);
            average = values.Average();
            Debug.Log(average);
        }
        if (distance > 0.5 && notAbs < 0)
        {
            return;
        }
        
        if (distance <= perfect && Input.GetKeyDown(watchFor))
        {
            Debug.Log("Perfect !");
            Destroy(nextNote.Dequeue().gameObject);
        }
        else if (distance <= good && Input.GetKeyDown(watchFor))
        {
            Debug.Log("good");
            Destroy(nextNote.Dequeue().gameObject);
        }
        else if (distance <= bad && Input.GetKeyDown(watchFor))
        {
            Debug.Log("bad...");
            Destroy(nextNote.Dequeue().gameObject);
        }
        else if (distance > 0.5 && notAbs > 0)
        {
            Debug.Log("missed");
            Destroy(nextNote.Dequeue().gameObject);
        }

    }
}