using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float verticalPadding = 1f;
    [SerializeField] float horizontalPadding = 1f;
    [SerializeField] [Range(0, 2)] int playerNumber = 0; // 0 = CPU

    float yMin;
    float yMax;

    void Start()
    {
        SetUpInitialPos();
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
    }

    void SetUpInitialPos()
    {
        Camera gameCamera = Camera.main;

        float xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + horizontalPadding;
        float xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - horizontalPadding;
        float yMid = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;

        if (playerNumber == 1)
        {
            transform.position = new Vector2(xMin, yMid);
        }
        else if (playerNumber == 2)
        {
            transform.position = new Vector2(xMax, yMid);
        }

    }

    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + verticalPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - verticalPadding;
    }

    void Move()
    {
        if (playerNumber == 0)
        {
            CPUMovement();
        }
        else
        {
            PlayerMovement();
        }
    }

    void CPUMovement()
    {
        float newPos = Mathf.Clamp(FindObjectOfType<Ball>().transform.position.y, yMin, yMax);

        transform.position = new Vector2(transform.position.x, newPos);
    }

    void PlayerMovement()
    {
        float input = Input.GetAxis(playerNumber == 1 ? "Vertical" : "Vertical2");
        float deltaY = input * speed * Time.deltaTime;
        float newPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(transform.position.x, newPos);
    }
}
