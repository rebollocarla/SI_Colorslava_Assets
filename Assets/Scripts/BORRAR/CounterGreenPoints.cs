using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterGreenPoints : MonoBehaviour
{
    public CounterValue counterValue;
    // Start is called before the first frame update
    void Start()
    {
        counterValue = GameObject.Find("CounterValue").GetComponent<CounterValue>();
    }

    public void AddScorep1()
    {
        counterValue.AddScorep1();
    }
    public void AddScore2()
    {
        counterValue.AddScorep2();
    }
}
