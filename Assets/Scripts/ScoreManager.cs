using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Text scoreNumberText;

    public Text comboText;

    public GameObject scoreTextPrefab;
    
    
    string excellent = "excellent";
    string good = "good";
    string meh = "meh";
    public string rank;

    public int score;
    public int combo;
    public int ScoreMax;
    int comboMax;

    public int bestCombo;
    string missed = "Missed";

    // Update is called once per frame
    public void UpdateScore(float time, InputManager manager)
    {

        Debug.Log("Distance : " + time);
        if (time <= 0.02)
        {
            Popup(excellent,new Color32(237, 227, 40, 255));
            if (manager.musician.currentNote.Count == 0)
            {
                manager.musician.currentNote = manager.musician.notesData.excellent;

                manager.musician.noteCounter = 0;
            }

            score = score + 3;
            combo++;
            if (combo >= 11) {
                combo = combo - 1;
            }
            if (combo >= 3) {
                score = score + combo;
            }

            if (combo >= bestCombo) bestCombo = combo;
        }
        else if (time <= 0.04)
        {
            Popup(good, new Color32(20, 163, 12, 255));
            if (manager.musician.currentNote.Count == 0)
            {
                manager.musician.currentNote = manager.musician.notesData.good;

                manager.musician.noteCounter = 0;
            }
            score = score + 2;
        }
        else if (time <= 0.117f)
        {
            Popup(meh, new Color32(219, 79, 35, 255));
            if (manager.musician.currentNote.Count == 0)
            {
                manager.musician.currentNote = manager.musician.notesData.meh;

                manager.musician.noteCounter = 0;
            }
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
            if (manager.musician.currentNote.Count == 0)
            {
                manager.musician.currentNote = manager.musician.notesData.missed;

                manager.musician.noteCounter = 0;
            }

            Popup(missed,new Color32(0,0,0,0));
            combo = 0;
        }
        ScoreMax = ScoreMax + 3 + comboMax;
        comboMax++;
        if (comboMax >= 11)
        {
            comboMax = comboMax - 1;
        }
        if (comboMax >= 3)
        {
            ScoreMax = ScoreMax + comboMax;
        }
        if (8 * score <= ScoreMax) {
            rank = "E";
        }
        if (8 * score >= ScoreMax && 7 * score <= ScoreMax){
            rank = "D";
        }
        if (7 * score >= ScoreMax && 6 * score <= ScoreMax) {
            rank = "C";
        }
        if (6 * score >= ScoreMax && 4 * score <= ScoreMax){
            rank = "B";
        }
        if (4 * score >= ScoreMax && 2 * score <= ScoreMax)
        {
            rank = "A";
            if (2 * score >= ScoreMax && score < ScoreMax){
                rank = "S";
            }
            if (score==ScoreMax){
                rank = "GOD";
            }
        }
    
    


        scoreNumberText.text = "Score : " + score;
        comboText.text = "Combo : " + combo;
    }

    void Popup(string msg, Color color)
    {
        var sc = Instantiate(scoreTextPrefab,scoreNumberText.transform.parent);
        var t = sc.GetComponent<ScoreText>();
        t.text = msg;
        t.color = color;
        t.Fire();
    }

}
