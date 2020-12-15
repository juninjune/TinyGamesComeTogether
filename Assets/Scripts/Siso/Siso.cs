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
    private GameObject deadZonePrefab;

    private Ball myBall;
    private Platform myPlatform;

    [SerializeField]
    private Vector2[] scorePositions;
    private List<Siso_score> scores = new List<Siso_score>();

    protected override void Awake()
    {
        base.Awake();

        Title = GAME_TITLE.SISO;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

        if (Input.GetKeyDown(KeyCode.Space))
            Resume();
    }
    
    public void SpawnScore()
    {
        Siso_score score;

        do
        {
            int index = Random.Range(0, scores.Count);
            score = scores[index];
        } while (score.GetIsActivated());

        score.Enable();
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

        SpawnScore();

        isInitialized = true;
        isPlaying = true;
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

    public override void Pause()
    {
        if (!isInitialized || !isPlaying)
            return;

        myBall.Pause();
        myPlatform.Pause();

        isPlaying = false;
    }

    public override void Resume()
    {
        if (!isInitialized || isPlaying)
            return;

        myBall.Resume();
        myPlatform.Resume();

        isPlaying = true; ;
    }
}
