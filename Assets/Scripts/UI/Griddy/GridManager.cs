using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject spawnSharedObject;
    public Transform spawnUnderThisObject;
    private TileInteractable[,] gridMatrix;
    
    public float size = 1f;
    public int width = 0;
    public int height = 0;

    private int localMouseX = 0;
    private int localMouseY = 0;    

    // Start is called before the first frame update
    void Start()
    {
        gridMatrix = new TileInteractable[width, height];
        for (int i = 0; i < width; i++) { 
            for (int j = 0; j < height; j++)
            {
                float x = i * size;
                float y = j * size;
                GameObject clone_tile = Instantiate(tile, new Vector3(x, y, transform.position.z), Quaternion.identity, transform);
                clone_tile.transform.localScale = new Vector3(size, size, size);
                clone_tile.name = $"Tile {i}, {j}";

                gridMatrix[i, j] = clone_tile.GetComponent<TileInteractable>();
                gridMatrix[i, j].spawnObject = spawnSharedObject;
                gridMatrix[i, j].parentGrid = this;
                gridMatrix[i, j].x_ind = i;
                gridMatrix[i, j].y_ind = j;
            }
        
        }

        transform.position = new Vector3(-(width * size - size) / 2, -(height * size - size) / 2, transform.position.z);
    }

    private void Update()
    {
        //Debug.Log($"{localMouseX}, {localMouseY}");
    }

    public int getLocalMouseX() { return localMouseX; }
    public int getLocalMouseY() { return localMouseY; }
    public TileInteractable[,] getTileInteractable() { return gridMatrix; }
    public void setLocalMouseX(int x) {  localMouseX = x; }
    public void setLocalMouseY(int y) { localMouseY = y; }
    



}
