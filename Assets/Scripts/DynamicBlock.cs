using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBlock : MonoBehaviour
{
    public float intervalo = 1f; // Intervalo de tiempo entre cambios de color
    public float duracionRojo = 0.5f; // Duración de la transición al color rojo
    public float duracionVerde = 0.5f; // Duración de la transición al color verde
    public int cuadradoSize = 4; // Tamaño del cuadrado rojo

    private GameObject[,] cuadradoRojo; // Matriz para almacenar los cuadrados rojos
    private GameObject[,] tablero; // Matriz para almacenar el tablero de cuadrados
    private Color colorBase = Color.green; // Color base verde
    private List<Renderer> rojosActivos = new List<Renderer>(); // Lista de renderers con color rojo activo
    private int currentPosX = 0; // Posición actual del cuadrado rojo en el eje X
    private int currentPosY = 0; // Posición actual del cuadrado rojo en el eje Y

    private void Start()
    {
        // Crear el tablero de cuadrados
        CreateTablero();

        // Comenzar la corutina para cambiar los colores de los cuadrados
        StartCoroutine(CambiarColorIluminacion());
    }

    private void CreateTablero()
    {
        // Obtener las dimensiones del tablero
        int width = cuadradoSize * 4;
        int height = cuadradoSize * 4;

        // Crear el array para almacenar los cuadrados
        tablero = new GameObject[width, height];

        // Crear los cuadrados en el tablero
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Crear un nuevo cuadrado
                GameObject square = GameObject.CreatePrimitive(PrimitiveType.Cube);
                square.transform.position = new Vector3(x, 0, y);
                square.tag = "Square";

                // Añadir el cuadrado al tablero
                tablero[x, y] = square;
            }
        }
    }

    private IEnumerator CambiarColorIluminacion()
    {
        while (true)
        {
            // Cambiar el color del cuadrado rojo actual
            ChangeCuadradoRojoColor();

            // Esperar un breve periodo con el cuadrado rojo activo
            yield return new WaitForSeconds(duracionRojo);

            // Desactivar el cuadrado rojo y volver a verde
            DisableCuadradoRojo();

            // Mover el cuadrado rojo a la siguiente posición
            MoveCuadradoRojo();

            // Esperar el intervalo de tiempo antes de cambiar los colores nuevamente
            yield return new WaitForSeconds(intervalo);
        }
    }

    private void ChangeCuadradoRojoColor()
    {
        // Cambiar el color de cada cuadrado del cuadrado rojo
        for (int x = currentPosX; x < currentPosX + cuadradoSize; x++)
        {
            for (int y = currentPosY; y < currentPosY + cuadradoSize; y++)
            {
                GameObject square = tablero[x, y];
                Renderer renderer = square.GetComponent<Renderer>();

                // Establecer el color base como verde
                renderer.material.color = colorBase;

                // Si el renderer está en la lista de rojos activos, no cambiar el color
                if (rojosActivos.Contains(renderer))
                    continue;

                // Iniciar la transición al color rojo
                StartCoroutine(TransicionColor(renderer, Color.red, duracionRojo));

                // Agregar el renderer a la lista de rojos activos
                rojosActivos.Add(renderer);
            }
        }
    }

    private void DisableCuadradoRojo()
    {
        // Desactivar los colores rojos y volver a verde en el cuadrado rojo
        for (int x = currentPosX; x < currentPosX + cuadradoSize; x++)
        {
            for (int y = currentPosY; y < currentPosY + cuadradoSize; y++)
            {
                GameObject square = tablero[x, y];
                Renderer renderer = square.GetComponent<Renderer>();

                StartCoroutine(TransicionColor(renderer, colorBase, duracionVerde));

                // Eliminar el renderer de la lista de rojos activos
                rojosActivos.Remove(renderer);
            }
        }
    }

    private void MoveCuadradoRojo()
    {
        // Mover el cuadrado rojo a la siguiente posición
        currentPosX++;
        currentPosY++;

        // Verificar si el cuadrado rojo se sale del tablero
        if (currentPosX + cuadradoSize > tablero.GetLength(0))
            currentPosX = 0;

        if (currentPosY + cuadradoSize > tablero.GetLength(1))
            currentPosY = 0;
    }

    private IEnumerator TransicionColor(Renderer renderer, Color targetColor, float duration)
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

