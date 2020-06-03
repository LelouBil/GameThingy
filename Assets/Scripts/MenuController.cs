using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{


    public GameObject rulesSplash;

    
    
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
