using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSquares : MonoBehaviour
{
    public GameObject squarePrefab;  // The prefab for the square object
    public GameObject plane;
    public float squareNumber;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        // Get the size of the plane object
        Vector3 planeSize = plane.GetComponent<Renderer>().bounds.size;
 
        float squareSize = planeSize.x / squareNumber;

        // Calculate the starting position for the grid
        Vector3 startPos = new Vector3(squareSize / 2.0f, 0.0f, squareSize / 2.0f);

        // Create a square object for each row and column
        for (int x = 0; x < planeSize.x; x = x + (int)squareSize)
        {
            for (int z = 0; z < planeSize.z; z = z + (int)squareSize)
            {
                // Create a new square object at the current position
                Vector3 pos = new Vector3(x, 0.0f, z) + startPos;
                GameObject square = Instantiate(squarePrefab, pos, Quaternion.identity);

                // Set the square object's scale to the specified size
                square.transform.localScale = new Vector3(squareSize, squareSize, squareSize);

                // Set the square object's parent to the grid object
                square.transform.parent = transform;
            }
        }
    }
}