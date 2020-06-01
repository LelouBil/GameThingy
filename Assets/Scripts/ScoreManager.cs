using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreNumberText;

    public GameObject scoreTextPrefab;
    
    
    string excellent = "excellent";
    string good = "good";
    string meh = "meh";

    int score;
    int combo;
    string missed = "Missed";

    // Update is called once per frame
    public void UpdateScore(float time, InputManager manager)
    {

        Debug.Log("Distance : " + time);
        if (time <= 0.02) {
            Popup(excellent);
            score=score+3;
            combo++;
            if (combo >= 3) {
                score = score + combo;
            }
        }
        else if (time <= 0.04)
        {
            Popup(good);
            score = score + 2;
        }
        else if (time <= 0.117f)
        {
            Popup(meh);
            score++;
            combo = 0;
        }
        else
        {
            if (manager.musician != null)
            {
                manager.musician.up = manager.musician.down;
                //manager.musician.counter = 0;
                manager.musician.transform.rotation = Quaternion.Euler(0, 0, 90);
                manager.musician.missCounter = 0;
            }

            Popup(missed);
            combo = 0;
        }

        scoreNumberText.text = "Score : " + score;
    }

    void Popup(string msg)
    {
        var sc = Instantiate(scoreTextPrefab,scoreNumberText.transform.parent);
        var t = sc.GetComponent<ScoreText>();
        t.text = msg;
        t.color = Color.white;
        t.Fire();
    }

}
