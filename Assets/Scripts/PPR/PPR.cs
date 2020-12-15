using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPR : Game
{
    bool isInGame = false;
    
    [SerializeField]
    GameObject comRunnerPrefab;
    [SerializeField]
    GameObject playerRunnerPrefab;

    PPR_Player player;
    PPR_Com com;
    PPR_Map map;
    PPR_UI ui;

    const float EndLine = -1385.0f;

    float timeInterval = 0.0f; // 이전 입력에서 지금 입력까지의 시간;

    protected override void Awake()
    {
        base.Awake();
        Title = GAME_TITLE.PPR;
    }

    private void Update()
    {
        timeInterval += Time.deltaTime;

        TryRun();
        UpdateClipSpeed();
        CheckPlayerArrive();

    }

    public override void Initialize()
    {
        base.Initialize();

        player = Instantiate(playerRunnerPrefab).GetComponent<PPR_Player>();
        map = FindObjectOfType<PPR_Map>();
        ui = GetComponent<PPR_UI>();


        player.Initialize();

        ui.CountDown();
    }

    public override void Pause()
    {
        
    }

    public override void Resume()
    {

    }

    public void StartRun()
    {
        isInGame = true;
    }

    bool isLastInputWasLeft = false;
    void TryRun()
    {
        if (!isInGame)
            return;

        if((Input.GetKeyDown(KeyCode.LeftArrow) && !isLastInputWasLeft)
            || (Input.GetKeyDown(KeyCode.RightArrow) && isLastInputWasLeft))
        {
            Run();
        }
    }

    void Run()
    {
        map.Run(1 / timeInterval);

        player.PlayRunClip();


        timeInterval = 0;

        isLastInputWasLeft = !isLastInputWasLeft;
    }

    void UpdateClipSpeed()
    {
        player.UpdateRunSpeed(-map.GetSpeed() * 0.2f);
    }

    void CheckPlayerArrive()
    {
        print(map.transform.localPosition.x);

        if(map.transform.localPosition.x < EndLine)
        {
            SwapGame();
        }
    }

}
