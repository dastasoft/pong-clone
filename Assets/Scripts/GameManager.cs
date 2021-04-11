using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;
    [SerializeField] Scoreboard scoreboard;
    [SerializeField] int pointsToWin = 10;

    bool leftSide = false;

    void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        Vector3 initialBallPos = GetCenterField();

        Ball ball = Instantiate(ballPrefab, new Vector2(initialBallPos.x, initialBallPos.y), Quaternion.identity) as Ball;

        ball.Impulse(new Vector2(leftSide ? 8 : -8, 2));

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
}
