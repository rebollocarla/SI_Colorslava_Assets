using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChange : MonoBehaviour
{
    private CounterValue counterGreen1;
    private CounterValue counterGreen2;
    private TimerLives counterLives1;
    private TimerLives counterLives2;
    private GameObject player1;
    private GameObject player2;

    public float redProbability = 0.8f; // Probabilidad de que un square sea pintado de rojo (20%)

    private void Start()
    {
        counterGreen1 = GameObject.Find("CounterValue").GetComponent<CounterValue>();
        counterGreen2 = GameObject.Find("CounterValue").GetComponent<CounterValue>();
        counterLives1 = GameObject.Find("TimerLives").GetComponent<TimerLives>();
        counterLives2 = GameObject.Find("TimerLives").GetComponent<TimerLives>();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        GetComponent<Renderer>().material.color = Color.green;
        // Verificar si el square debe ser pintado de rojo
        if (Random.value <= redProbability)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //colorOriginal = renderer.material.color;
        if (GetComponent<Renderer>().material.color == Color.green)
        {
            if (collision.gameObject == player1)
            {
                counterGreen1.AddScorep1();
            }
            else if (collision.gameObject == player2)
            {
                counterGreen2.AddScorep2();
            }
        } 
        else if (GetComponent<Renderer>().material.color == Color.red)
        {
            if (collision.gameObject == player1)
            {
                counterLives1.SubstractLifep1();
            }
            else if (collision.gameObject == player2)
            {
                counterLives2.SubstractLifep2();
            }
        }
        GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().material.color = Color.green; // cambiar
    }

}


