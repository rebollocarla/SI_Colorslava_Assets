using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private float timer = 120;
    public TMP_Text textoTimer;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        textoTimer.text = "Time: " + timer.ToString("f0");
        if (textoTimer.text == "Time: 0"){
            SceneManager.LoadScene("EndGame");
        }
    }
}
