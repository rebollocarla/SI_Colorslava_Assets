using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}