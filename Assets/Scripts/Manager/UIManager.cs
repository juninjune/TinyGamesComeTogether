using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject title;
    [SerializeField]
    private GameObject dead;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text endScore;
    [SerializeField]
    private Text bestScore;

    private void Start()
    {
        SetTitleScreenOn();
    }

    public void SetTitleScreenOn()
    {
        DisableAllScreen();

        title.SetActive(true);
    }

    public void SetTitleScreenOff()
    {
        title.SetActive(false);
    }

    public void SetDeadScreenOn()
    {
        DisableAllScreen();

        bestScore.text = "Best : " + PlayerPrefs.GetInt("bestScore");
        endScore.text = "Score : " + GameManager.instance.GetScore();


        dead.SetActive(true);
    }

    public void UpdateScore()
    {
        int score = GameManager.instance.GetScore();

        scoreText.text = "SCORE : " + score;
    }

    void DisableAllScreen()
    {
        title.SetActive(false);
        dead.SetActive(false);
    }
}
