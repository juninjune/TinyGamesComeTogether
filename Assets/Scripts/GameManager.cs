using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    private Game[] games;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        games = FindObjectsOfType<Game>();

        foreach (Game game in games)
        {
            game.OnScore += ScoreUp;
        }
    }

    private void ScoreUp()
    {
        score++;
        print("점수를 얻었습니다. 현재 점수는 " + score + "점 입니다.");
    }

    public void GameOver()
    {
        print("게임 오버입니다.");
    }
}
