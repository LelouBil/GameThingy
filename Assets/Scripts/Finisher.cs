using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finisher : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator animator;

    public Text FinalScoreText;
    
    public Text FinalComboText;

    public GameManager gameManager;

    public Text HighScoreText;
    
    public Text HighComboText;

    public Text BeatHighScoreText;
    
    private static readonly int Finish1 = Animator.StringToHash("Finish");

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private bool beaten = false;

    public void Finish(GameManager gameManager)
    {
        this.gameManager = gameManager;
        animator.SetTrigger(Finish1);
        Debug.Log("finish");
    }

    public void RealFinish()
    {
        FinalScoreText.text = "Score final : " + gameManager.scoreManager.score;
        FinalComboText.text = "Plus long combo : " + gameManager.scoreManager.bestCombo;

        int highscore = PlayerPrefs.GetInt("highscore",0);

        HighScoreText.text = "Meilleur score : " + highscore;

        HighComboText.text = "Rang : " + gameManager.scoreManager.rank;

        if (gameManager.scoreManager.score > highscore)
        {
            PlayerPrefs.SetInt("highscore",gameManager.scoreManager.score);
            BeatHighScoreText.text = "Vous avez battu votre meilleur score !";
            beaten = true;
        }

        StartCoroutine(ShowTexts());
    }

    private IEnumerator ShowTexts()
    {
        Show(FinalScoreText);
        yield return new WaitForSeconds(0.5f);
        Show(FinalComboText);
        yield return new WaitForSeconds(0.8f);
        Show(HighScoreText);
        yield return new WaitForSeconds(1.2f);
        Show(HighComboText);
        if (beaten)
        {
            yield return new WaitForSeconds(1.2f);
            Show(BeatHighScoreText);
        }
    }

    private void Show(Text text)
    {
        text.color = Color.white;
    }
}
