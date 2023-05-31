using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSquares2 : MonoBehaviour
{
    public float intervalo = 1f;
    public float duracionRojo = 0.5f;
    public float duracionVerde = 0.5f;
    public int columnasPorFila = 12;
    GameObject[] squares;
    private Color colorBase = Color.green;
    private List<Renderer> rojosActivos = new List<Renderer>();
    private int filaActual = 0;

    private void Start()
    {
        squares = GameObject.FindGameObjectsWithTag("Square");
        if (squares.Length > 0)
        {
            StartCoroutine(CambiarColorIluminacion());
        }
        else
        {
            Debug.LogWarning("No se encontraron cuadrados con el tag 'Square'. Asegúrate de que los cuadrados existan y estén etiquetados correctamente.");
        }
    }

    private IEnumerator CambiarColorIluminacion()
    {
        while (true)
        {
            int primeraColumna = filaActual % columnasPorFila;
            int ultimaColumna = primeraColumna + (columnasPorFila * (squares.Length / columnasPorFila - 1));

            for (int i = primeraColumna; i <= ultimaColumna; i += columnasPorFila)
            {
                GameObject square = squares[i];
                Renderer renderer = square.GetComponent<Renderer>();
                if (renderer != null)
                {
                    StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                    rojosActivos.Add(renderer);
                }
            }

            yield return new WaitForSeconds(duracionRojo);

            foreach (Renderer renderer in rojosActivos)
            {
                StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));
            }
            rojosActivos.Clear();

            filaActual++;

            if (filaActual >= columnasPorFila)
            {
                filaActual = 0;
                yield return new WaitForSeconds(intervalo);
            }
        }
    }

    private IEnumerator TransicionColor(Renderer renderer, Color targetColor, float duration)
    {
        Color initialColor = renderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            Color lerpedColor = Color.Lerp(initialColor, targetColor, progress);
            renderer.material.color = lerpedColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor;
    }
}
