using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siso : Game
{
    [SerializeField]
    private GameObject sisoPrefab;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private GameObject scorePrefab;
    [SerializeField]
    private GameObject triggerScorePrefab;
    [SerializeField]
    private GameObject deadZonePrefab;

    private Ball myBall;
    private Platform myPlatform;

    [SerializeField]
    private Vector2[] scorePositions;
    private List<Siso_score> scores = new List<Siso_score>();
    private List<GameObject> triggers = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        Title = GAME_TITLE.SISO;
    }

    public void SpawnScore()
    {
        if(Random.value < 0.2f)
        {
            SpawnTrigger();
            return;
        }

        Siso_score score;

        do
        {
            int index = Random.Range(0, scores.Count);
            score = scores[index];
        } while (score.GetIsActivated());

        score.Enable();
    }

    void SpawnTrigger()
    {
        GameObject trigger;

        do
        {
            int index = Random.Range(0, triggers.Count);
            trigger = triggers[index];
        } while (trigger.activeSelf);

        trigger.SetActive(true);
    }

    public override void Initialize()
    {
        base.Initialize();

        myBall = Instantiate(ballPrefab).GetComponent<Ball>();
        myPlatform = Instantiate(sisoPrefab).GetComponent<Platform>();
        Instantiate(deadZonePrefab).transform.parent = transform;
        myBall.transform.parent = transform;
        myPlatform.transform.parent = transform;

        InitializeScores();
        InitializeTriggers();

        SpawnScore();

        isInitialized = true;
        IsStarted = false;
    }

    public override void StartGame()
    {
        myBall.Resume();
        myPlatform.Resume();

        IsStarted = true;
    }

    private void InitializeScores()
    {
        for (int i = 0; i < scorePositions.Length; i++)
        {
            Siso_score score = Instantiate(scorePrefab, scorePositions[i], Quaternion.identity).GetComponent<Siso_score>();
            score.transform.parent = transform;
            scores.Add(score);
        }
    }

    private void InitializeTriggers()
    {
        for (int i = 0; i < scorePositions.Length; i++)
        {
            GameObject trigger = Instantiate(triggerScorePrefab, scorePositions[i], Quaternion.identity);
            trigger.transform.parent = transform;
            triggers.Add(trigger);
            trigger.SetActive(false);
        }
    }

    public override void Pause()
    {
        base.Pause();

        myBall.Pause();
        myPlatform.Pause();

        isPlaying = false;
    }

    public override void Resume()
    {
        myBall.Resume();
        myPlatform.Resume();

        isPlaying = true; ;
    }
}
