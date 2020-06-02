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
    string rank;

    int score;
    int combo;
    int Scoremax;
    int comboMax;
    string missed = "Missed";

    // Update is called once per frame
    public void UpdateScore(float time, InputManager manager)
    {

        Debug.Log("Distance : " + time);
        if (time <= 0.02) {
            Popup(excellent);
            score = score + 3;
            combo++;
            if (combo >= 11) {
                combo = combo - 1;
            }
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
        Scoremax = Scoremax + 3 + comboMax
        comboMax++
        if (comboMax >= 11)
            {
                comboMax = comboMax - 1;
            }
            if (comboMax >= 3)
            {
                Scoremax = Scoremax + comboMax;
            }
        if (8 * score <= ScoreMax) {
            rank = "E"
               }
        if (8 * score >= ScoreMax && 7 * score <= ScoreMax){
            rank = "D"
               }
        if (7 * score >= ScoreMax && 6 * score <= ScoreMax) {
            rank = "C"
               }
        if (6 * score >= ScoreMax && 4 * score <= ScoreMax){
                rank = "B"
               }
        if (4 * score >= ScoreMax && 2 * score <= ScoreMax)
        {
            rank = "A"
        if (2 * score >= ScoreMax && score < ScoreMax){
                rank = "S"
               }
        if (score==scoreMax){
                rank = "GOD"
               }
        }
    }
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
