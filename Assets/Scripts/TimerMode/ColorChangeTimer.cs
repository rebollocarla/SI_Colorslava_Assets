using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorChangeTimer : MonoBehaviour
{
    [SerializeField] private TimerLivesmode2 counter_total;
    private GameObject player1;
    private GameObject player2;
    public string sceneName;

    public float redProbability = 0.8f; // Probabilidad de que un square sea pintado de rojo (20%)

    private void Start()
    {
        counter_total = GameObject.Find("TimerLives2").GetComponent<TimerLivesmode2>();
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
        
        if (GetComponent<Renderer>().material.color == Color.red)
        {
            if (collision.gameObject == player1)
            {
                counter_total.SubstractLife();
            }
            if (collision.gameObject == player2)
            {
                counter_total.SubstractLife();
            }
        }

        GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().material.color = Color.green; // cambiar
    }

}



