using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameType
    {
        OnePlayer,
        TwoPlayer
    }

    [SerializeField] Ball ballPrefab;
    [SerializeField] Paddle paddlePrefab;
    [SerializeField] int pointsToWin = 10;
    [SerializeField] GameType gameType;
    Scoreboard scoreboard;
    bool leftSide;

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
        SpawnPlayers();
        SpawnBall();
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
            FindObjectOfType<SceneLoader>().LoadEndScene();
        }
        else
        {
            SpawnBall();
        }

    }

    void UpdateScore(string wallName)
    {
        string side = wallName == "Left Wall" ? "left" : "right";

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
