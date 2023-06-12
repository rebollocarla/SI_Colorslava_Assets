using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerLives : MonoBehaviour
{
    public TMP_Text scoreTextp1;
    public TMP_Text scoreTextp2;
    public int lives1 = 3;
    public int lives2 = 3;
    public AudioSource audioError;

    // Start is called before the first frame update
    void Start()
    {
        scoreTextp1 = GameObject.Find("LivesPlayer1").GetComponent<TMP_Text>();
        scoreTextp2 = GameObject.Find("LivesPlayer2").GetComponent<TMP_Text>();
        audioError = GameObject.Find("AudioError").GetComponent<AudioSource>();
    }

    public void SubstractLifep1()
    {
        lives1 -= 1;
        audioError.Play();
        if (lives1 == 0)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void SubstractLifep2()
    {
        lives2 -= 1;
        audioError.Play();
        if (lives2 == 0)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void UpdateLifep1()
    {
        scoreTextp1.text = "Lives 1: " + lives1.ToString();
    }

    public void UpdateLifep2()
    {
        scoreTextp2.text = "Lives 2: " + lives2.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifep1();
        UpdateLifep2();
    }
}