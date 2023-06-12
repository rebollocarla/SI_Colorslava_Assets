using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerLivesmode2 : MonoBehaviour
{
    public TMP_Text scoreText;
    public int total_lives = 5;
    public AudioSource audioError;
    

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Total_lives").GetComponent<TMP_Text>();
        audioError = GameObject.Find("AudioError").GetComponent<AudioSource>();
    }

    public void SubstractLife()
    {
        total_lives -= 1;
        audioError.Play();
        if (total_lives == 0)
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void UpdateLives()
    {
        scoreText.text = "TOTAL LIVES LEFT: " + total_lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLives();
    }
}
