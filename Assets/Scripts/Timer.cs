using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 120;
    public TMP_Text textoTimer;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        textoTimer.text = "Time: " + timer.ToString("f0");
    }
}
