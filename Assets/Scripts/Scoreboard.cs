using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] GameObject leftScore;
    [SerializeField] GameObject rightScore;

    public void Reset()
    {
        leftScore.GetComponent<Text>().text = "0";
        rightScore.GetComponent<Text>().text = "0";
    }

    public void IncreaseScore(string side)
    {
        GameObject scorePanel = side == "left" ? rightScore : leftScore;
        Text scoreText = scorePanel.GetComponent<Text>();

        scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
    }

    public int GetLeftScore()
    {
        return int.Parse(leftScore.GetComponent<Text>().text);
    }

    public string GetWinner()
    {
        return int.Parse(leftScore.GetComponent<Text>().text) > int.Parse(rightScore.GetComponent<Text>().text) ? "Player 1" : "Player 2";
    }

    public int GetRightScore()
    {
        return int.Parse(rightScore.GetComponent<Text>().text);
    }
}
