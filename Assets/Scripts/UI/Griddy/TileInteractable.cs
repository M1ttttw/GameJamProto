using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TileInteractable : MonoBehaviour
{
    public SpriteRenderer rend;
    private Color color;

    public GridManager parentGrid;
    public GameObject spawnObject; // Object with the Shop Item
    private GameObject gridObjInTile; // Object with the grid item
    private ShopItem shopItmInTile; // Storing the shopitem that is in the tile.

    public int x_ind;
    public int y_ind;

    private bool drag = false;

    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rend.color = color;
        if (gridObjInTile != null && !drag)
        {
            gridObjInTile.transform.position = transform.position;
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log($"Mouse has entered on {this.gameObject.name}");
        color = new Color(0, 0, 0, 0.4f);
        spawnObject = parentGrid.shopItemPrefab;
    }

    private void OnMouseExit()
    {
        Debug.Log($"Mouse has exited on {this.gameObject.name}");
        color = new Color(0, 0, 0, 0);
    }

    private void OnMouseDown()
    {
        Debug.Log($"Mouse has clicked on {this.gameObject.name}");
        color = new Color(0, 0, 0, 0.7f);
        if (gridObjInTile == null)
        {
            gridObjInTile = Instantiate(spawnObject.GetComponent<ShopItem>().gridItemPrefab, transform.position, Quaternion.identity, parentGrid.gridItemParent);
            gridObjInTile.GetComponent<GridItem>().parent = parentGrid.prefabParent;
            shopItmInTile = spawnObject.GetComponent<ShopItem>();
            parentGrid.shopManager.boughtItem(shopItmInTile);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log($"Mouse has been released on {this.gameObject.name}");
        if (drag)
        {
            color = new Color(0, 0, 0, 0);

            if (gridObjInTile != null)
            {
                int currMouseX = parentGrid.getLocalMouseX();
                int currMouseY = parentGrid.getLocalMouseY();

                if (!(currMouseX == x_ind && currMouseY == y_ind))
                {
                    // Mouse was released on a location that is not this tile.
                    TileInteractable released_tile = parentGrid.getTileInteractable()[currMouseX, currMouseY];

                    if (released_tile.gridObjInTile == null)
                    {
                        // Move the object here.
                        released_tile.gridObjInTile = gridObjInTile;
                        gridObjInTile = null;

                        released_tile.shopItmInTile = shopItmInTile;
                        shopItmInTile = null;

                        Debug.Log("Moved object");
                    }
                    else
                    {
                        // Otherwise swap the objects
                        GameObject temp1GridObj = gridObjInTile;
                        GameObject temp2GridObj = released_tile.gridObjInTile;
                        ShopItem temp1ShopItm = shopItmInTile;
                        ShopItem temp2ShopItm = released_tile.shopItmInTile;

                        released_tile.gridObjInTile = temp1GridObj;
                        gridObjInTile = temp2GridObj;

                        released_tile.shopItmInTile = temp1ShopItm;
                        shopItmInTile = temp2ShopItm;

                        Debug.Log("Swapped Object");
                    }
                }
            }

        }
        else
        {
            color = new Color(0, 0, 0, 0.4f);
        }
        drag = false;
    }

    private void OnMouseDrag()
    {
        Debug.Log($"Mouse is being dragged on {this.gameObject.name}");
        drag = true;
        if (gridObjInTile != null)
        {
            // Make the object follow the cursor
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            gridObjInTile.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, gridObjInTile.transform.position.z);
        }


    }

    private void OnMouseOver()
    {
        parentGrid.setLocalMouseX(x_ind);
        parentGrid.setLocalMouseY(y_ind);

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Right Clicked {name}");
            if (gridObjInTile != null && !drag)
            {
                Destroy(gridObjInTile);
                gridObjInTile = null;
                parentGrid.shopManager.soldItem(shopItmInTile);
                shopItmInTile = null;
            }
        }
    }

    public GameObject getGridObjInTile() { return gridObjInTile; }
}
