using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Game
{
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CheckInput();

        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();

        if (Input.GetKeyDown(KeyCode.Space))
            Resume();
    }

    public override void Initialize()
    {
        StartCoroutine(InitializeCoroutine());
    }

    IEnumerator InitializeCoroutine()
    {
        yield return null;

        InitializeMap();

        yield return null;

        InitializePlayer();
        PutScorePoint();

        timeCoroutine = TimeGo();
        StartCoroutine(timeCoroutine);

        isPlaying = true;
    }

    public override void Pause()
    {
        if (!isPlaying)
            return;

        StopCoroutine(timeCoroutine);
        isPlaying = false;
    }

    public override void Resume()
    {
        if (isPlaying)
            return;

        timeCoroutine = TimeGo();
        StartCoroutine(timeCoroutine);

        isPlaying = true;
    }

    public override void GameOver()
    {
        base.GameOver();
        Pause();
    }

    #region Map

    private Tile[][] map;
    [SerializeField]
    private GameObject tilePrefab;

    const int MapSizeX = 11;
    const int MapSizeY = 11;

    void InitializeMap()
    {
        map = new Tile[MapSizeX][];
        for (int i = 0; i < MapSizeX; i++)
        {
            map[i] = new Tile[MapSizeY];
            for (int j = 0; j < MapSizeY; j++)
            {
                map[i][j] = Instantiate(tilePrefab, new Vector3(i - 5, j - 5), Quaternion.identity).GetComponent<Tile>();
                map[i][j].Pos = new Vector2Int(i, j);
            }
        }
    }

    TILE_STATE empty = TILE_STATE.EMPTY;
    TILE_STATE body = TILE_STATE.BODY;
    TILE_STATE score = TILE_STATE.SCORE;

    Tile GetRandomEmptyTile()
    {
        Tile tile;

        do
        {
            int x = Random.Range(0, MapSizeX);
            int y = Random.Range(0, MapSizeY);

            tile = map[x][y];
        } while (!tile.GetIsEmpty());

        return tile;
    }

    Tile GetTile(Vector2Int _pos)
    {
        return map[_pos.x][_pos.y];
    }

    bool CheckOutOfIndex(Vector2Int _input, out Tile target)
    {
        if (_input.x < 0 || _input.x >= MapSizeX)
        {
            target = null;
            return false;
        }

        if (_input.y < 0 || _input.y >= MapSizeY)
        {
            target = null;
            return false;
        }

        target = GetTile(_input);
        return true;
    }

    #endregion

    #region Player

    private Queue<Tile> snake = new Queue<Tile>();
    private Tile head;
    private Vector2Int currentDirection;
    private Vector2Int nextDirection;
    private Tile scorePoint;

    Vector2Int UP = new Vector2Int(0, 1);

    void InitializePlayer()
    {
        head = map[5][5];
        head.SetTileState(body);
        snake.Enqueue(head);
        currentDirection = UP;
        nextDirection = currentDirection;
    }

    void PutScorePoint()
    {
        scorePoint = GetRandomEmptyTile();
        scorePoint.SetTileState(score);
    }

    void CheckInput()
    {
        int input = (int)Input.GetAxisRaw("Horizontal");

        if(input != 0)
            nextDirection = new Vector2Int(currentDirection.y * input, -currentDirection.x * input);
    }

    void TryMove()
    {

        if (CheckOutOfIndex(head.Pos + nextDirection, out head)
            && !snake.Contains(head))
            Move();
        else
            GameOver();
    }

    void Move()
    {
        snake.Enqueue(head);
        head.SetTileState(body);

        currentDirection = nextDirection;

        if(head == scorePoint)
        {
            ScoreUp();
            PutScorePoint();
            return;
        }

        snake.Dequeue().SetTileState(empty);
    }

    #endregion

    #region Time

    float timeStep = 0.5f;

    public void SetTimeStep(float _timeStep)
    {
        timeStep = _timeStep;
    }

    IEnumerator timeCoroutine;

    IEnumerator TimeGo()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeStep);
            TryMove();
        }
    }

    #endregion
}
