using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterValue : MonoBehaviour
{
    public TMP_Text scoreText1;
    public TMP_Text scoreText2;
    public int score1 = 0;
    public int score2 = 0;
  
    // Start is called before the first frame update
    void Start()
    {
        scoreText1 = GameObject.Find("CounterPlayer1").GetComponent<TMP_Text>();
        scoreText2 = GameObject.Find("CounterPlayer2").GetComponent<TMP_Text>();
    }

    public void AddScorep1()
    {
        score1 += 1;
    }

    public void AddScorep2()
    {
        score2 += 1;
    }

    public void UpdateScorep1()
    {
        scoreText1.text = "Score 1: " + score1.ToString();
    }

    public void UpdateScorep2()
    {
        scoreText2.text = "Score 2: " + score2.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateScorep1();
        UpdateScorep2();
    }
}
