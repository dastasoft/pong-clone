using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Ball ballPrefab;

    void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        Camera gameCamera = Camera.main;
        Vector3 initialBallPos = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        Instantiate(ballPrefab, new Vector2(initialBallPos.x, initialBallPos.y), Quaternion.identity);
    }

    public void Goal()
    {
        // TODO Increase Score

        SpawnBall();
    }
}
