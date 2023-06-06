using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorChange : MonoBehaviour
{
    public CounterGreenPoints counterGreen;
    public float redProbability = 0.8f; // Probabilidad de que un square sea pintado de rojo (20%)
    public Color defaultColor = Color.green; // Color por defecto de los squares
    public Color hitColor = Color.black; // Color cuando un square es impactado por una pelota
    //private bool isTouchingSquare = false; // Indica si la pelota está en contacto con un square

    private void Start()
    {
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
            counterGreen.AddScore(1);
        }

        GetComponent<Renderer>().material.color = hitColor;
    }


    private void OnCollisionExit(Collision collision)
    {
        //isTouchingSquare = false;
        GetComponent<Renderer>().material.color = defaultColor;
    }

}


