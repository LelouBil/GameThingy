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
    public void UpdateScore(float time)
    {

        if (time <= 0.2) {
            Popup(excellent);
            score=score+3;
            combo++;
            if (combo >= 3) {
                score = score + combo;
            }
        }
        else if (time <= 0.4 && time > 0.2)
        {
            Popup(good);
            score = score + 2;
        }
        else if (time <= 0.5 && time > 0.4)
        {
            Popup(meh);
            score++;
            combo = 0;
        }
        else if (time > 0.5)
        {
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
