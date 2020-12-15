using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pong : Game
{
    protected override void Awake()
    {
        base.Awake();

        Title = GAME_TITLE.PONG;
    }

    public override void Initialize()
    {
        
    }

    public override void Pause()
    {

    }

    public override void Resume()
    {

    }

    public override void GameOver()
    {
        base.GameOver();
    }
}
