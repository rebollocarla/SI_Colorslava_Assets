using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public static Lives Instance;
    public GameObject gameOver;
    public UnityEngine.UI.Text lifesRemaning;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
    }
    
public void UpdateLifes()
{
    lifesRemaning.text = Lives.Instance.lifesRemaning.ToString();
}


    public void ShowGameOverPlane(){
        gameOver.SetActive(true);
    }

}



