using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    protected bool isPlaying = false;

    public event Action OnScore;

    private Camera myCamera;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    public abstract void Initialize();
    public abstract void Pause();
    public abstract void Resume();

    public virtual void GameOver()
    {
        GameManager.instance.GameOver();
    }

    public Camera GetCamera()
    {
        return myCamera;
    }

    protected void ScoreUp()
    {
        OnScore?.Invoke();
    }
}
