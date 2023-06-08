using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerLives : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public TMP_Text scoreTextp1;
    public TMP_Text scoreTextp2;
    private int livesp1 = 3;
    private int livesp2 = 3;

    // Start is called before the first frame update
    void Start()
    {

        scoreTextp1 = GameObject.Find("LivesPlayer1").GetComponent<TMP_Text>();
        scoreTextp2 = GameObject.Find("LivesPlayer2").GetComponent<TMP_Text>();


    }

    public void SubstractScorep1(int newScore)
    {
        livesp1 -= newScore;
    }

    public void SubstractScorep2(int newScore)
    {
        livesp2 -= newScore;
    }

    public void UpdateScorep1()
    {
        scoreTextp1.text = "lives: " + livesp1.ToString();
    }

    public void UpdateScorep2()
    {
        scoreTextp2.text = "lives: " + livesp2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScorep1();
        UpdateScorep2();

    }
}
