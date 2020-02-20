using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculduscore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 

    }
    string excellent = "excellent";
    string good = "good";
    string meh = "meh";

    int score;

    // Update is called once per frame
    void Updatescore(float time)
    {

        if (time <= 0.2) {
            Popup(excellent);
            score=score+3;
        }
        if (time <= 0.4 && time > 0.2)
        {
            Popup(good);
            score = score + 2;
        }
        if (time <= 0.5 && time > 0.4)
        {
            Popup("meh");
            score++;
        }
    }

    void Popup(string msg)
    {
        Debug.log();
    }

}
