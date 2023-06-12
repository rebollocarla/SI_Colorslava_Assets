using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloorPattern2 : MonoBehaviour
{
    public float intervalo = 1f; // Intervalo de tiempo entre cambios de color
    public float duracionRojo = 0.5f; // Duración de la transición al color rojo
    public float duracionVerde = 0.5f; // Duración de la transición al color verde
    public int columnasPorFila = 12; // Número de columnas por fila
    GameObject[] squares; // Array para almacenar los cuadrados existentes
    private List<Renderer> rojosActivos = new List<Renderer>(); // Lista de renderers con color rojo activo

    private void Awake()
    {
        // Obtener todos los objetos con el tag "Square"
        squares = GameObject.FindGameObjectsWithTag("SquareTimer");
        if (squares.Length > 0)
        {
            // Comenzar la corutina para cambiar los colores de los cuadrados en el plano
            StartCoroutine(RealizarCambios());
        }
        else
        {
            Debug.LogWarning("No se encontraron cuadrados con el tag 'Square'. Asegúrate de que los cuadrados existan y estén etiquetados correctamente.");
        }
    }

    private IEnumerator RealizarCambios()
    {
        while (true)
        {
            // Cambiar el color de cada bloque vertical de 12 squares
            for (int i = 0; i < columnasPorFila; i++)
            {
                // Calcular los índices de la primera y última columna del bloque vertical actual
                int primeraColumna = i;
                int ultimaColumna = primeraColumna + columnasPorFila * (squares.Length / columnasPorFila - 1);

                // Recopilar los renderizadores del bloque vertical actual
                List<Renderer> renderersFilaActual = new List<Renderer>();
                Dictionary<Renderer, Color> coloresOriginales = new Dictionary<Renderer, Color>();

                for (int j = primeraColumna; j <= ultimaColumna; j += columnasPorFila)
                {
                    GameObject square = squares[j];
                    Renderer renderer = square.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersFilaActual.Add(renderer);
                        coloresOriginales[renderer] = renderer.material.color;
                    }
                }
                // Iniciar la transición al color rojo para todo el bloque vertical
                foreach (Renderer renderer in renderersFilaActual)
                {
                    StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                    rojosActivos.Add(renderer);
                }

                // Esperar un breve periodo con los colores rojos activos
                yield return new WaitForSeconds(duracionRojo);

                // Establecer el color base como verde si el bloque anterior ha sido pintado completamente de rojo
            
                foreach (Renderer renderer in rojosActivos)
                {
                    Color colorOriginal = coloresOriginales[renderer];
                    StartCoroutine(TransicionColor(renderer, colorOriginal, duracionVerde));
                }
                rojosActivos.Clear();
            }
            yield return new WaitForSeconds(intervalo);

            // Cambiar el color de cada bloque horizontal de 12 squares
            for (int i = 0; i < columnasPorFila; i++)
            {
                // Calcular los índices de la primera y última fila del bloque horizontal actual
                int primeraFila = i * columnasPorFila;
                int ultimaFila = primeraFila + columnasPorFila - 1;

                // Recopilar los renderizadores del bloque horizontal actual
                List<Renderer> renderersColumnaActual = new List<Renderer>();
                Dictionary<Renderer, Color> coloresOriginales = new Dictionary<Renderer, Color>();
                for (int j = primeraFila; j <= ultimaFila; j++)
                {
                    GameObject square = squares[j];
                    Renderer renderer = square.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersColumnaActual.Add(renderer);
                        coloresOriginales[renderer] = renderer.material.color;
                    }
                }

                // Iniciar la transición al color rojo para todo el bloque horizontal
                foreach (Renderer renderer in renderersColumnaActual)
                {
                    StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                    rojosActivos.Add(renderer);
                }

                // Esperar un breve periodo con los colores rojos activos
                yield return new WaitForSeconds(duracionRojo);

                // Desactivar los colores rojos y volver a verde en el bloque horizontal actual
                foreach (Renderer renderer in rojosActivos)
                {
                    Color colorOriginal = coloresOriginales[renderer];
                    StartCoroutine(TransicionColor(renderer, colorOriginal, duracionVerde));
                }
                rojosActivos.Clear();            
            }

            // Esperar un breve periodo antes de reiniciar el ciclo
            yield return new WaitForSeconds(intervalo);
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