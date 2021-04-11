using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float timeToDespawn = 0.5f;
    public void Impulse(Vector2 direction)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = direction;
    }

    public float GetTimeToDespawn()
    {
        return timeToDespawn;
    }
}
