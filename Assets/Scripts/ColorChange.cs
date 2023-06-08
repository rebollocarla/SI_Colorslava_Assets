using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChange : MonoBehaviour
{
    public CounterValue counterGreen1;
    public CounterValue counterGreen2;
    public TimerLives counterLives1;
    public TimerLives counterLives2;
    public GameObject player1;
    public GameObject player2;

    public float redProbability = 0.8f; // Probabilidad de que un square sea pintado de rojo (20%)
    public Color defaultColor = Color.green; // Color por defecto de los squares
    public Color hitColor = Color.black; // Color cuando un square es impactado por una pelota
    //private bool isTouchingSquare = false; // Indica si la pelota está en contacto con un square

    private void Start()
    {

        counterGreen1 = GameObject.Find("CounterValue").GetComponent<CounterValue>();
        counterGreen2 = GameObject.Find("CounterValue").GetComponent<CounterValue>();
        // Pintar el square de verde
        GetComponent<Renderer>().material.color = defaultColor;

        // Verificar si el square debe ser pintado de rojo
        if (Random.value <= redProbability)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Pintar el square de cyan
        //isTouchingSquare = true;
        if (GetComponent<Renderer>().material.color == Color.green)
        {
            if (collision.gameObject == player1)
            {
                counterGreen1.AddScore(1);
            }
            else if (collision.gameObject == player2)
            {
                counterGreen2.AddScore(1);
            }
        }

        if (GetComponent<Renderer>().material.color == Color.red)
        {
            if (collision.gameObject == player1)
            {
                counterLives1.SubstractScorep1(1);
            }
            else if (collision.gameObject == player2)
            {
                counterLives2.SubstractScorep2(1);
            }

        }
        GetComponent<Renderer>().material.color = hitColor;
    }


    private void OnCollisionExit(Collision collision)
    {
        //isTouchingSquare = false;
        GetComponent<Renderer>().material.color = defaultColor;
    }

}


