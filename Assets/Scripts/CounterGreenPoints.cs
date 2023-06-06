using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterGreenPoints : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;
    public int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    public void UpdateScore()
    {
        scoreText.text = "score 0" + score;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
}
