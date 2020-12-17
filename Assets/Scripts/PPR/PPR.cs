using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPR : Game
{
    bool isRunning = false;

    [SerializeField]
    GameObject mapPrefab;
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
        if (!isRunning)
            return;

        UpdateTimeInterval();
        TryRun();
        UpdateClipSpeed();
        CheckPlayerArrive();
    }

    public override void Initialize()
    {
        base.Initialize();

        player = Instantiate(playerRunnerPrefab).GetComponent<PPR_Player>();
        player.transform.parent = transform;

        ui = Instantiate(mapPrefab).GetComponent<PPR_UI>();
        ui.GetComponent<RectTransform>().SetParent(transform);

        map = ui.GetComponentInChildren<PPR_Map>();
        map.Initialize();

        player.Initialize();

        IsStarted = false;
    }

    public override void StartGame()
    {
        ui.CountDown();
        IsStarted = true;
    }

    public override void Pause()
    {
        base.Pause();

        player.Pause();
        map.Pause();

        isRunning = false;
    }

    public override void Resume()
    {
        player.Resume();
        map.Resume();

        isRunning = true;
    }

    void ResetGame()
    {
        Destroy(player.gameObject);
        Destroy(ui.gameObject);

        Initialize();

        isRunning = false;
        IsStarted = false;
    }

    public void StartRun()
    {
        isRunning = true;
    }

    public void StopRun()
    {
        isRunning = false;
    }

    void UpdateTimeInterval()
    {
        timeInterval += Time.deltaTime;
    }

    bool isLastInputWasLeft = false;
    void TryRun()
    {
        if((Input.GetKeyDown(KeyCode.LeftArrow) && !isLastInputWasLeft)
            || (Input.GetKeyDown(KeyCode.RightArrow) && isLastInputWasLeft))
        {
            Run();

            isLastInputWasLeft = !isLastInputWasLeft;
        }
    }

    void Run()
    {
        map.Run(1 / timeInterval);

        player.PlayRunClip();

        timeInterval = 0;
    }

    void UpdateClipSpeed()
    {
        player.UpdateRunSpeed(-map.GetSpeed() * 0.2f);
    }

    void CheckPlayerArrive()
    {
        if(map.transform.localPosition.x < EndLine && isRunning)
        {
            ResetGame();
            PlayNextScene();
        }
    }

}
