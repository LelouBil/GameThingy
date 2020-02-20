using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMover : MonoBehaviour
{

    public float movespeed;

    public InputManager Manager;

    public static float goal = -7.3f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Manager != null)
        {
            Manager.nextNote.Enqueue(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3( (-movespeed) * Time.deltaTime, 0, 0);
    }
}
