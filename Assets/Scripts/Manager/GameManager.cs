using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAME_TITLE
{
    PONG,
    SNAKE,
    SISO,
    PPR
}

[Serializable]
public class Scene
{
    [SerializeField]
    private GAME_TITLE[] titles;

    public void PlayScene()
    {
        if (titles.Length == 0 || titles.Length > 2)
        {
            Debug.LogError("유효하지 않은 배열입니다.");
                return;
        }

        if(titles.Length == 1)
        {
            GameManager.instance.SetFeaturedGames(titles[0]);
        }

        if (titles.Length == 2)
        {
            GameManager.instance.SetFeaturedGames(titles[0], titles[1]);
        }
    }
}

public class GameManager : MonoBehaviour
{
    public event Action OnGameover;

    private int score = 0;
    private bool isPlaying = false;

    private Game[] games;
    public Dictionary<GAME_TITLE, Game> gameDic = new Dictionary<GAME_TITLE, Game>();

    private LayoutController layoutController;

    public static GameManager instance;

    public Scene[] scenario;
    private int currentSceneIndex = 0;

    GAME_TITLE Pong = GAME_TITLE.PONG;
    GAME_TITLE Ppr = GAME_TITLE.PPR;
    GAME_TITLE Siso = GAME_TITLE.SISO;
    GAME_TITLE Snake = GAME_TITLE.SNAKE;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        layoutController = GetComponent<LayoutController>();

        InitializeGame();
        layoutController.Initialize();

        PlayNextScene();

        isPlaying = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPlaying)
            Restart();
    }

    public void PlayNextScene()
    {
        scenario[currentSceneIndex++].PlayScene();
    }


    void InitializeGame()
    {
        InitializeDictionary();
        SubscribeScoreUp();
        foreach (Game game in games)
        {
            game.Initialize();
        }
    }

    void InitializeDictionary()
    {
        games = FindObjectsOfType<Game>();

        foreach  (Game game in games)
        {
            gameDic.Add(game.Title, game);
        }
    }

    void SubscribeScoreUp()
    {
        foreach (Game game in games)
        {
            game.OnScore += ScoreUp;
        }
    }

    public void SetFeaturedGames(GAME_TITLE _name)
    {
        PauseAllGames();

        gameDic[_name].Resume();

        layoutController.SetFeaturedGames(_name);
    }

    public void SetFeaturedGames(GAME_TITLE _name, GAME_TITLE _name2)
    {
        PauseAllGames();

        gameDic[_name].Resume();
        gameDic[_name2].Resume();

        layoutController.SetFeaturedGames(_name, _name2);
    }

    void SetFeaturedGames(GAME_TITLE _name, GAME_TITLE _name2, GAME_TITLE _name3, GAME_TITLE _name4)
    {
        PauseAllGames();

        gameDic[_name].Resume();
        gameDic[_name2].Resume();
        gameDic[_name3].Resume();
        gameDic[_name4].Resume();

        layoutController.SetFeaturedGames(_name, _name2, _name3, _name4);
    }

    void PauseAllGames()
    {
        foreach (Game game in games)
        {
            game.Pause();
        }
    }

    private void ScoreUp()
    {
        score++;
        print("점수를 얻었습니다. 현재 점수는 " + score + "점 입니다.");
    }

    public void GameOver()
    {
        if (!isPlaying)
            return;

        isPlaying = false;
        OnGameover.Invoke();
        print("게임 오버입니다.");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
