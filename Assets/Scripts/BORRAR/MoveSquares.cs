using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquares : MonoBehaviour
{
    // Intervalo de tiempo entre cambios de color
    public float intervalo = 1f;
    // Rango máximo de la posición en el eje X y Z
    public GameObject plane;
    GameObject[] squares;
    public Color defaultColor = Color.green; // Color por defecto de los squares
    public Color hitColor = Color.black; // Color cuando un square es impactado por una pelota
    private bool isTouchingSquare = false; 

    // Start is called before the first frame update
    void Start()
    {
        // Obtener todos los objetos con el tag "Square"
        squares = GameObject.FindGameObjectsWithTag("Square");
        // Comenzar la corutina para cambiar los colores de los cuadrados dinámicos
        StartCoroutine(CambiarColores());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Pintar el square de cyan
        //isTouchingSquare = true;
        GetComponent<Renderer>().material.color = hitColor;
    }


    private void OnCollisionExit(Collision collision)
    {
        //isTouchingSquare = false;
        GetComponent<Renderer>().material.color = defaultColor;
    }

    private System.Collections.IEnumerator CambiarColores()
    {
        // Get the size of the plane object
        Vector3 planeSize = plane.GetComponent<Renderer>().bounds.size;

        while (true)
        {
            // Cambiar el color y la posición de cada cuadrado dinámico
            foreach (GameObject square in squares)
            {
                Renderer renderer = square.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Generar un color aleatorio (rojo o verde)
                    Color nuevoColor = Random.Range(0, 2) == 0 ? Color.red : Color.green;
                    renderer.material.color = nuevoColor;
                }
            }

            // Esperar el intervalo de tiempo antes de cambiar los colores y posiciones nuevamente
            yield return new WaitForSeconds(intervalo);
        }
    }
}