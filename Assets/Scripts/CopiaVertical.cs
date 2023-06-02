using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CopiaVertical : MonoBehaviour
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
            // Comenzar la corutina para cambiar los colores de los cuadrados en el plano
            StartCoroutine(CambiarColorPlano());
        }
        else
        {
            Debug.LogWarning("No se encontraron cuadrados con el tag 'Square'. Asegúrate de que los cuadrados existan y estén etiquetados correctamente.");
        }
    }


    private System.Collections.IEnumerator CambiarColorPlano()
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
                List<Renderer> renderersBloqueVertical = new List<Renderer>();
                for (int j = primeraColumna; j <= ultimaColumna; j += columnasPorFila)
                {
                    GameObject square = squares[j];
                    Renderer renderer = square.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersBloqueVertical.Add(renderer);
                    }
                }

                // Establecer el color base como verde si el bloque anterior ha sido pintado completamente de rojo
                if (rojosActivos.Count >= columnasPorFila)
                {
                    foreach (Renderer rojo in rojosActivos)
                    {
                        StartCoroutine(TransicionColor(rojo, colorBase, duracionVerde));
                    }
                    rojosActivos.Clear();
                }

                // Iniciar la transición al color rojo para todo el bloque vertical
                foreach (Renderer renderer in renderersBloqueVertical)
                {
                    StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                    rojosActivos.Add(renderer);
                }

                // Esperar un breve periodo con los colores rojos activos
                yield return new WaitForSeconds(duracionRojo);

                // Desactivar los colores rojos y volver a verde en el bloque vertical actual
                foreach (Renderer renderer in rojosActivos)
                {
                    StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));
                }
                rojosActivos.Clear();
            }

            // Cambiar el color de cada bloque horizontal de 12 squares
            for (int i = 0; i < columnasPorFila; i++)
            {
                // Calcular los índices de la primera y última fila del bloque horizontal actual
                int primeraFila = i * columnasPorFila;
                int ultimaFila = primeraFila + columnasPorFila - 1;

                // Recopilar los renderizadores del bloque horizontal actual
                List<Renderer> renderersBloqueHorizontal = new List<Renderer>();
                for (int j = primeraFila; j <= ultimaFila; j++)
                {
                    GameObject square = squares[j];
                    Renderer renderer = square.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderersBloqueHorizontal.Add(renderer);
                    }
                }

                // Establecer el color base como verde si el bloque anterior ha sido pintado completamente de rojo
                if (rojosActivos.Count >= columnasPorFila)
                {
                    foreach (Renderer rojo in rojosActivos)
                    {
                        StartCoroutine(TransicionColor(rojo, colorBase, duracionVerde));
                    }
                    rojosActivos.Clear();
                }

                // Iniciar la transición al color rojo para todo el bloque horizontal
                foreach (Renderer renderer in renderersBloqueHorizontal)
                {
                    StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                    rojosActivos.Add(renderer);
                }

                // Esperar un breve periodo con los colores rojos activos
                yield return new WaitForSeconds(duracionRojo);

                // Desactivar los colores rojos y volver a verde en el bloque horizontal actual
                foreach (Renderer renderer in rojosActivos)
                {
                    StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));
                }
                rojosActivos.Clear();
            }

            // Esperar un breve periodo antes de reiniciar el ciclo
            yield return new WaitForSeconds(intervalo);
        }
    }


    private System.Collections.IEnumerator CambiarColorVertical()
    {
        while (filaActual == 0)
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
    }

    private IEnumerator CambiarColorHorizontal()
    {
        Dictionary<Renderer, Color> initialColors = new Dictionary<Renderer, Color>(); // Diccionario para almacenar los colores iniciales

        while (true)
        {
            int primeraColumna = filaActual % columnasPorFila;
            int ultimaColumna = primeraColumna + (columnasPorFila * (squares.Length / columnasPorFila - 1));

            // Almacenar los colores iniciales de los cuadrados en el bloque horizontal actual
            for (int i = primeraColumna; i <= ultimaColumna; i += columnasPorFila)
            {
                GameObject square = squares[i];
                Renderer renderer = square.GetComponent<Renderer>();
                if (renderer != null && !initialColors.ContainsKey(renderer))
                {
                    initialColors[renderer] = renderer.material.color; // Almacenar el color inicial en el diccionario si no existe
                }
            }

            // Cambiar los colores de los cuadrados al color rojo
            foreach (Renderer renderer in initialColors.Keys)
            {
                StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));
                rojosActivos.Add(renderer);
            }

            yield return new WaitForSeconds(duracionRojo);

            // Restaurar los colores iniciales de los cuadrados
            foreach (Renderer renderer in initialColors.Keys)
            {
                StartCoroutine(TransicionColor(renderer, initialColors[renderer], duracionVerde));
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
