using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject shopItemPrefab;
    public Transform tileInterParent;
    public Transform gridItemParent;
    public Transform prefabParent;
    private TileInteractable[,] tileMatrix;
    
    public float size = 1f;
    public int width = 0;
    public int height = 0;

    private int localMouseX = 0;
    private int localMouseY = 0;    

    public ShopManager shopManager;

    // Start is called before the first frame update
    void Start()
    {
        tileMatrix = new TileInteractable[width, height];
        for (int i = 0; i < width; i++) { 
            for (int j = 0; j < height; j++)
            {
                float x = i * size;
                float y = j * size;
                GameObject clone_tile = Instantiate(tile, new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z), Quaternion.identity, tileInterParent);
                clone_tile.transform.localScale = new Vector3(size, size, size);
                clone_tile.name = $"Tile {i}, {j}";

                tileMatrix[i, j] = clone_tile.GetComponent<TileInteractable>();
                tileMatrix[i, j].spawnObject = shopItemPrefab;
                tileMatrix[i, j].parentGrid = this;
                tileMatrix[i, j].x_ind = i;
                tileMatrix[i, j].y_ind = j;
            }
        
        }

        transform.position = new Vector3(transform.position.x - (width * size - size) / 2, transform.position.y - (height * size - size) / 2, transform.position.z);
    }

    void Update()
    {
        shopItemPrefab = shopManager.getItemInDisplay();
    }

    [ContextMenu("Transition to combat phase")]
    public void transToCombat() { 
        // Call this when you transition from placement to combat.
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++) {
                GameObject obj = tileMatrix[i, j].getGridObjInTile();
                if (obj != null) {
                    tileMatrix[i, j].setPrefabInTile(obj.GetComponent<GridItem>().spawnPrefab());
                    obj.SetActive(false);
                }
                tileMatrix[i, j].gameObject.SetActive(false);
            }
        }
    }

    [ContextMenu("Transition to placement phase")]
    public void transToPlacement() {
        // Call this when you transition back from combat to placement.
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject gridObj = tileMatrix[i, j].getGridObjInTile();
                GameObject prefabObj = tileMatrix[i, j].getPrefabInTile();
                if (gridObj != null)
                {
                    gridObj.SetActive(true);
                    if (prefabObj == null) {
                        // Check if the car is dead, thus we need to remove the grid obj there.
                        Debug.Log($"Removed dead car at {i}, {j}");
                        tileMatrix[i, j].setGridObjInTile(null);
                        tileMatrix[i, j].setShopItmInTile(null);

                        // Then destroy the grid object
                        Destroy(gridObj);
                    }
                    
                } 
                tileMatrix[i, j].gameObject.SetActive(true);
            }
        }
    }

    public int getLocalMouseX() { return localMouseX; }
    public int getLocalMouseY() { return localMouseY; }
    public TileInteractable[,] getTileInteractable() { return tileMatrix; }
    public void setLocalMouseX(int x) {  localMouseX = x; }
    public void setLocalMouseY(int y) { localMouseY = y; }
    



}
