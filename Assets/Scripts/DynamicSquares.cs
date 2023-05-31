using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSquares : MonoBehaviour
{
    public float intervalo = 1f; // Intervalo de tiempo entre cambios de color
    public float duracionRojo = 0.5f; // Duración de la transición al color rojo
    public float duracionVerde = 0.5f; // Duración de la transición al color verde
    public int columnasPorFila = 12; // Número de columnas por fila
    GameObject[] squares; // Array para almacenar los cuadrados existentes
    private Color colorBase = Color.green; // Color base verde
    private List<Renderer> rojosActivos = new List<Renderer>(); // Lista de renderers con color rojo activo
    private int filaActual = 0; // Índice de la fila actual

    private void Start()
    {
        // Obtener todos los objetos con el tag "Square"
        squares = GameObject.FindGameObjectsWithTag("Square");
        if (squares.Length > 0)
        {
            // Comenzar la corutina para cambiar los colores de los cuadrados
            StartCoroutine(CambiarColorIluminacion());
        }
        else
        {
            Debug.LogWarning("No se encontraron cuadrados con el tag 'Square'. Asegúrate de que los cuadrados existan y estén etiquetados correctamente.");
        }
    }

    private System.Collections.IEnumerator CambiarColorIluminacion()
    {
        while (true)
        {
            // Calcular los índices de la primera y última columna de la fila actual
            int primeraColumna = filaActual * columnasPorFila;
            int ultimaColumna = primeraColumna + columnasPorFila - 1;

            // Recopilar los renderizadores de la fila actual
            List<Renderer> renderersFilaActual = new List<Renderer>();
            for (int i = primeraColumna; i <= ultimaColumna; i++)
            {
                GameObject square = squares[i];
                Renderer renderer = square.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderersFilaActual.Add(renderer);
                }
            }

            // Establecer el color base como verde si la fila anterior ha sido pintada completamente de rojo
            if (rojosActivos.Count >= columnasPorFila)
            {
                foreach (Renderer rojo in rojosActivos)
                {
                    StartCoroutine(TransicionColor(rojo, colorBase, duracionVerde));
                }
                rojosActivos.Clear();
            }

            // Iniciar la transición al color rojo para toda la fila
            foreach (Renderer renderer in renderersFilaActual)
            {
                StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                rojosActivos.Add(renderer);
            }

            // Esperar un breve periodo con los colores rojos activos
            yield return new WaitForSeconds(duracionRojo);

            // Desactivar los colores rojos y volver a verde en la fila actual
            foreach (Renderer renderer in rojosActivos)
            {
                StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));
            }
            rojosActivos.Clear();

            // Incrementar el índice de la fila actual
            filaActual++;

            // Verificar si se ha alcanzado el final de las filas
            if (filaActual * columnasPorFila >= squares.Length)
            {
                filaActual = 0; // Reiniciar el índice de la fila
                yield return new WaitForSeconds(intervalo); // Esperar el intervalo de tiempo antes de reiniciar el ciclo
            }
        }
    }

    private System.Collections.IEnumerator TransicionColor(Renderer renderer, Color targetColor, float duration)
    {
        Color initialColor = renderer.material.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calcular el factor de progreso de la transición
            float progress = elapsedTime / duration;

            // Interpolar entre el color inicial y el color objetivo
            Color lerpedColor = Color.Lerp(initialColor, targetColor, progress);

            // Actualizar el color del renderer
            renderer.material.color = lerpedColor;

            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Esperar al siguiente frame
            yield return null;
        }

        // Asegurarse de establecer el color objetivo exactamente al final de la transición
        renderer.material.color = targetColor;
    }
}