using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterValue : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score = 0;
    public int maxScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GameObject.Find("CounterForCounterGameMode").GetComponent<TMP_Text>();

    }

    public void AddScore(int newScore)
    {

        score += newScore;
        Debug.Log(score);
    }

    public void UpdateScore()
    {
        scoreText.text = "score 0" + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }
}
