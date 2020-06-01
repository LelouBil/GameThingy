using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{

    public string text;

    public Color color;

    public Animator animator;
    private static readonly int Fire1 = Animator.StringToHash("Fire");

    private void Start()
    {
        GetComponent<Text>().text = text;
        GetComponent<Text>().color = color;
    }

    public void Fire()
    {
        animator.SetTrigger(Fire1);
    }

    public void Ended()
    {
        animator.Rebind();
        Destroy(this.gameObject);
    }
}