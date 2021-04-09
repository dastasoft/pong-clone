using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float verticalPadding = 1f;
    [SerializeField] float horizontalPadding = 1f;

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
        float xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + horizontalPadding;
        float yMid = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;

        transform.position = new Vector2(xMin, yMid);
    }

    void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + verticalPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - verticalPadding;
    }

    void Move()
    {
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float newPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(transform.position.x, newPos);
    }
}
