using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{


    public GameObject rulesSplash;

    public Text scoreText;

    private void Start()
    {
        scoreText.text = "Meilleur\nscore : " + PlayerPrefs.GetFloat("highscore", 0);
    }

    public void Quiter()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        rulesSplash.SetActive(true);
    }

    private void Update()
    {
        if (rulesSplash.activeInHierarchy && Input.anyKeyDown)
        {
            SceneManager.LoadScene("Scenes/Ecran");
        }
    }
}
