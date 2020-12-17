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
    private UIManager ui;

    public static GameManager instance;

    public Scene[] scenario;
    private int currentSceneIndex = 0;


    //GAME_TITLE Pong = GAME_TITLE.PONG;
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
        ui = FindObjectOfType<UIManager>();

        InitializeGame();
        layoutController.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isPlaying)
            Restart();
    }

    public void StartGame()
    {
        ui.SetTitleScreenOff();

        PlayNextScene();

        isPlaying = true;
    }

    public void PlayNextScene()
    {
        if (currentSceneIndex >= scenario.Length)
        {
            SetRandomGames();
            return;
        }

        scenario[currentSceneIndex++].PlayScene();
    }

    public int GetScore()
    {
        return score;
    }

    void InitializeGame()
    {
        InitializeDictionary();
        SubscribeScoreUp();
        foreach (Game game in games)
        {
            game.Initialize();
        }
        PauseAllGames();
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

    void SetRandomGames()
    {
        if(UnityEngine.Random.value < 0.5f) // 한 개 실행
        {
            int _case = UnityEngine.Random.Range(0, 3);

            switch (_case)
            {
                case 0:
                    SetFeaturedGames(Ppr);
                    break;
                case 1:
                    SetFeaturedGames(Snake);
                    break;
                case 2:
                    SetFeaturedGames(Siso);
                    break;
                default:
                    break;
            }
        }
        else // 두 개 실행
        {
            int _case = UnityEngine.Random.Range(0, 3);

            switch (_case)
            {
                case 0:
                    SetFeaturedGames(Ppr, Snake);
                    break;
                case 1:
                    SetFeaturedGames(Snake, Siso);
                    break;
                case 2:
                    SetFeaturedGames(Siso, Ppr);
                    break;
                default:
                    break;
            }
        }            
    }

    public void SetFeaturedGames(GAME_TITLE _name)
    {
        PauseAllGames();

        if (gameDic[_name].IsStarted)
            gameDic[_name].Resume();
        else
            gameDic[_name].StartGame();

        layoutController.SetFeaturedGames(_name);
    }

    public void SetFeaturedGames(GAME_TITLE _name, GAME_TITLE _name2)
    {
        PauseAllGames();

        if (gameDic[_name].IsStarted)
            gameDic[_name].Resume();
        else
            gameDic[_name].StartGame();

        if (gameDic[_name2].IsStarted)
            gameDic[_name2].Resume();
        else
            gameDic[_name2].StartGame();

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
        ui.UpdateScore();
    }

    public void GameOver()
    {
        if (!isPlaying)
            return;

        isPlaying = false;
        OnGameover.Invoke();

        int previousBestScore = PlayerPrefs.GetInt("bestScore");
        if (previousBestScore < score)
            PlayerPrefs.SetInt("bestScore", score);

        ui.SetDeadScreenOn();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
