using UnityEngine;

public class ClickManager : MonoBehaviour
{

    public GameManager gameManager;

    public void Click()
    {
        gameManager.Click();
    }

    public void end()
    {
        gameManager.EndAnim();
    }
}