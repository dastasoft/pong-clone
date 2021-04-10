using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject, 0.5f);
        FindObjectOfType<GameManager>().Goal();
    }
}
