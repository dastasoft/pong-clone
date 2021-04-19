using UnityEngine;
using UnityEngine.UI;

public class LoadWinner : MonoBehaviour
{
    [SerializeField] Text winner;
    // Start is called before the first frame update
    void Start()
    {
        winner.text = FindObjectOfType<GameManager>().winner + " wins!";
    }

}
