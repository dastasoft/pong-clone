using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameType
    {
        OnePlayer,
        TwoPlayer
    }

    enum WallNames
    {
        TopWall,
        BottomWall,
        LeftGoal,
        RightGoal
    }

    [SerializeField] Ball ballPrefab;
    [SerializeField] Paddle paddlePrefab;
    [SerializeField] GameObject wallPrefab;
    [SerializeField] GameObject goalPrefab;
    [SerializeField] int pointsToWin = 10;
    [SerializeField] GameType gameType;
    Scoreboard scoreboard;
    bool leftSide;
    public string winner = "";

    void Awake()
    {
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;

        if (gameManagerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    public void GameStart()
    {
        leftSide = true;
        SpawnWalls();
        SpawnPlayers();
        SpawnBall();
    }

    void SpawnWalls()
    {
        Camera gameCamera = Camera.main;
        float top = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y;
        float bottom = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, 0)).y;
        float left = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float right = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        GameObject topWall = Instantiate(wallPrefab, new Vector2(0, top + 0.5f), Quaternion.identity);
        topWall.GetComponent<BoxCollider2D>().size = new Vector2(right - left, 1f);
        topWall.name = WallNames.TopWall.ToString();

        GameObject bottomWall = Instantiate(wallPrefab, new Vector2(0, bottom - 0.5f), Quaternion.identity);
        bottomWall.GetComponent<BoxCollider2D>().size = new Vector2(right - left, 1f);
        bottomWall.name = WallNames.BottomWall.ToString();

        GameObject leftGoal = Instantiate(goalPrefab, new Vector2(left, 0), Quaternion.identity);
        leftGoal.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, top - bottom);
        leftGoal.name = WallNames.LeftGoal.ToString();

        GameObject rightGoal = Instantiate(goalPrefab, new Vector2(right, 0), Quaternion.identity);
        rightGoal.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, top - bottom);
        rightGoal.name = WallNames.RightGoal.ToString();
    }
    void SpawnPlayers()
    {
        float horizontalPadding = paddlePrefab.GetHorizontalPadding();
        Camera gameCamera = Camera.main;
        float xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + horizontalPadding;
        float xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - horizontalPadding;
        float yMid = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;

        Paddle player1 = Instantiate(paddlePrefab, new Vector2(xMin, yMid), Quaternion.identity);

        player1.SetPlayerNumber(1);

        Paddle player2 = Instantiate(paddlePrefab, new Vector2(xMax, yMid), Quaternion.identity);

        player2.SetPlayerNumber(gameType == GameType.OnePlayer ? 0 : 2);
    }

    void SpawnBall()
    {
        Vector3 initialBallPos = GetCenterField();

        Ball ball = Instantiate(ballPrefab, new Vector2(initialBallPos.x, initialBallPos.y), Quaternion.identity) as Ball;

        ball.Impulse(new Vector2(leftSide ? -8 : 8, 2));

        leftSide = !leftSide;
    }

    Vector3 GetCenterField()
    {
        Camera gameCamera = Camera.main;
        return gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
    }

    public void Goal(string wallName)
    {
        UpdateScore(wallName);

        if (CheckWinCondition())
        {
            winner = scoreboard.GetWinner();
            FindObjectOfType<SceneLoader>().LoadEndScene();
        }
        else
        {
            SpawnBall();
        }

    }

    void UpdateScore(string wallName)
    {
        string side = wallName == WallNames.LeftGoal.ToString() ? "left" : "right";

        scoreboard.IncreaseScore(side);
    }

    bool CheckWinCondition()
    {
        return scoreboard.GetLeftScore() >= pointsToWin || scoreboard.GetRightScore() >= pointsToWin;
    }

    public void Set1PlayerMode()
    {
        gameType = GameType.OnePlayer;
    }

    public void Set2PlayerMode()
    {
        gameType = GameType.TwoPlayer;
    }
}
