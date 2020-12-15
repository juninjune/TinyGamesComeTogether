using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    public event Action OnScore;
    public GAME_TITLE Title { get; set; }

    protected bool isInitialized = false;
    protected bool isPlaying = false;
    private Camera myCamera;

    protected virtual void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    public virtual void Initialize() 
    {
        if (isInitialized)
            return;

        GameManager.instance.OnGameover += GameOver;
    }
    public abstract void Pause();
    public abstract void Resume();

    public void SwapGame()
    {
        GameManager.instance.PlayNextScene();
    }

    public virtual void GameOver()
    {
        GameManager.instance.GameOver();
        Pause();
    }

    public Camera GetCamera()
    {
        return myCamera;
    }

    public void ScoreUp()
    {
        OnScore?.Invoke();
    }
}
