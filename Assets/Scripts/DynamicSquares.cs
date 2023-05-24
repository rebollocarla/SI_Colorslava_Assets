using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSquares : MonoBehaviour
{
    public float intervalo = 1f; // Intervalo de tiempo entre cambios de color
    public float duracionRojo = 0.5f; // Duración de la transición al color rojo
    public float duracionVerde = 0.5f; // Duración de la transición al color verde
    GameObject[] squares; // Array para almacenar los cuadrados existentes
    private Color colorBase = Color.green; // Color base verde
    private List<Renderer> rojosActivos = new List<Renderer>(); // Lista de renderers con color rojo activo

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
            // Cambiar el color de cada cuadrado existente
            foreach (GameObject square in squares)
            {
                Renderer renderer = square.GetComponent<Renderer>();
                if (renderer != null)
                {
                    // Establecer el color base como verde
                    renderer.material.color = colorBase;

                    // Si el renderer está en la lista de rojos activos, no cambiar el color
                    if (rojosActivos.Contains(renderer))
                        continue;

                    // Iniciar la transición al color rojo
                    yield return StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));

                    // Agregar el renderer a la lista de rojos activos
                    rojosActivos.Add(renderer);
                }
            }

            // Esperar un breve periodo con los colores rojos activos
            yield return new WaitForSeconds(duracionRojo);

            // Desactivar los colores rojos y volver a verde
            foreach (Renderer renderer in rojosActivos)
            {
                StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));
            }

            // Limpiar la lista de rojos activos
            rojosActivos.Clear();

            // Esperar el intervalo de tiempo antes de cambiar los colores nuevamente
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

    private void OnCollistionEnter(Collision collision)
    {
        GetComponent<MeshRenderer>().material.color = Color.cyan;
    }
}
