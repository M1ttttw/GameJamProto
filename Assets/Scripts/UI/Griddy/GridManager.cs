using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cam;
    public GameObject tile;
    public GameObject gridItemPrefab;
    public Transform tileInterParent;
    public Transform gridItemParent;
    public Transform prefabParent;
    private TileInteractable[,] tileMatrix;
    
    public float size = 1f;
    public int width = 0;
    public int height = 0;

    private int localMouseX = 0;
    private int localMouseY = 0;    

    // Start is called before the first frame update
    void Start()
    {
        tileMatrix = new TileInteractable[width, height];
        for (int i = 0; i < width; i++) { 
            for (int j = 0; j < height; j++)
            {
                float x = i * size;
                float y = j * size;
                GameObject clone_tile = Instantiate(tile, new Vector3(x, y, transform.position.z), Quaternion.identity, tileInterParent);
                clone_tile.transform.localScale = new Vector3(size, size, size);
                clone_tile.name = $"Tile {i}, {j}";

                tileMatrix[i, j] = clone_tile.GetComponent<TileInteractable>();
                tileMatrix[i, j].spawnObject = gridItemPrefab;
                tileMatrix[i, j].parentGrid = this;
                tileMatrix[i, j].x_ind = i;
                tileMatrix[i, j].y_ind = j;
            }
        
        }

        transform.position = new Vector3(cam.transform.position.x - (width * size - size) / 2, cam.transform.position.y - (height * size - size) / 2, transform.position.z);
        Debug.Log($"{cam.transform.position}");
    }

    [ContextMenu("Hide Tiles")]
    public void hideTiles() { 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tileMatrix[i, j].gameObject.SetActive(false);
            }
        }
    }

    [ContextMenu("Show Tiles")]
    public void showTiles() {
        for (int i = 0; i < width; i++) { 
            for (int j = 0; j < height; j++)
            {
                tileMatrix[i, j].gameObject.SetActive(true);
            }
        }
    }


    [ContextMenu("Spawn Prefabs in Tiles")]
    public void spawnPrefabs() { 
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++) {
                GameObject obj = tileMatrix[i, j].getObjectInTile();
                if (obj != null) { 
                    obj.GetComponent<GridItem>().spawnPrefab();
                }
            }
        }
    }

    public int getLocalMouseX() { return localMouseX; }
    public int getLocalMouseY() { return localMouseY; }
    public TileInteractable[,] getTileInteractable() { return tileMatrix; }
    public void setLocalMouseX(int x) {  localMouseX = x; }
    public void setLocalMouseY(int y) { localMouseY = y; }
    



}
