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
        
        if (Input.GetKey(watchFor))
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else{
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        if (nextNote.Count == 0) return;
        float notAbs = transform.position.x - nextNote.Peek().transform.position.x;
        float distance = Math.Abs(notAbs);
        //Get distance!
        bool triggered = false;
        if (Input.GetKeyDown(watchFor))
        {
            if (distance > 0.18f && notAbs < 0)
            {
                return;
            }

            triggered = true;
            
            gameManager.scoreManager.UpdateScore(distance,this);
            var n = nextNote.Dequeue();
            Destroy(n.gameObject);
        }
        
        
        if (distance > 0.117 && notAbs > 0)
        {
            Debug.Log("missed");
            gameManager.scoreManager.UpdateScore(0.20f,this);
            if(triggered) return;
            Destroy(nextNote.Dequeue().gameObject);
        }
        
    }
}