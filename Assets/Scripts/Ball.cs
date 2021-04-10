using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float timeToDespawn = 0.5f;

    void Start()
    {
        Impulse();
    }

    void Impulse()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 2);
    }
}
