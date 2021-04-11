using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject, FindObjectOfType<Ball>().GetTimeToDespawn());
        FindObjectOfType<GameManager>().Goal(gameObject.name);
    }
}
