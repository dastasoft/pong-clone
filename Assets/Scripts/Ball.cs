using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAndImpulse();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnAndImpulse()
    {
        Spawn();
        InitialImpulse();
    }

    void Spawn()
    {
        Camera gameCamera = Camera.main;
        Vector3 newPos = gameCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        transform.position = new Vector2(newPos.x, newPos.y);
    }

    void InitialImpulse()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 1);
    }
}
