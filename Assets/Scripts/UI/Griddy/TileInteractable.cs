using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TileInteractable : MonoBehaviour
{
    public SpriteRenderer rend;

    public GridManager parentGrid;
    public GameObject spawnObject;
    private GameObject objectInTile;

    public int x_ind;
    public int y_ind;

    private bool drag = false;

    // Start is called before the first frame update
    void Start()
    {
        rend.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectInTile != null && !drag)
        {
            objectInTile.transform.position = transform.position;
        }

    }
    private void OnMouseEnter()
    {
        Debug.Log($"Mouse has entered on {this.gameObject.name}");
        rend.color = new Color(0, 0, 0, 0.4f);
        // spawnObject = parentGrid.gridItemPrefab;
    }

    private void OnMouseExit()
    {
        Debug.Log($"Mouse has exited on {this.gameObject.name}");
        rend.color = new Color(0, 0, 0, 0);
    }

    private void OnMouseDown()
    {
        Debug.Log($"Mouse has clicked on {this.gameObject.name}");
        rend.color = new Color(0, 0, 0, 0.7f);
        //if (objectInTile == null)
        //{
        //    objectInTile = Instantiate(spawnObject, transform.position, Quaternion.identity, parentGrid.gridItemParent);
        //    objectInTile.GetComponent<GridItem>().parent = parentGrid.prefabParent;
        //}
    }

    private void OnMouseUp()
    {
        Debug.Log($"Mouse has been released on {this.gameObject.name}");
        if (drag)
        {
            rend.color = new Color(0, 0, 0, 0);

            //if (objectInTile != null)
            //{
            //    int currMouseX = parentGrid.getLocalMouseX();
            //    int currMouseY = parentGrid.getLocalMouseY();

            //    if (!(currMouseX == x_ind && currMouseY == y_ind))
            //    {
            //        // Mouse was released on a location that is not this tile.
            //        TileInteractable released_tile = parentGrid.getTileInteractable()[currMouseX, currMouseY];

            //        if (released_tile.objectInTile == null)
            //        {
            //            // Move the object here.
            //            released_tile.objectInTile = objectInTile;
            //            objectInTile = null;

            //            Debug.Log("Moved object");
            //        }
            //        else
            //        {
                        // Otherwise swap the objects
            //            GameObject temp1 = objectInTile;
            //            GameObject temp2 = released_tile.objectInTile;

            //            released_tile.objectInTile = temp1;
            //            objectInTile = temp2;

            //            Debug.Log("Swapped Object");
            //        }
            //    }
            //}

        }
        else
        {
            rend.color = new Color(0, 0, 0, 0.4f);
        }
        drag = false;
    }

    private void OnMouseDrag()
    {
        Debug.Log($"Mouse is being dragged on {this.gameObject.name}");
        drag = true;
        if (objectInTile != null)
        {
            // Make the object follow the cursor
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            objectInTile.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, objectInTile.transform.position.z);
        }


    }

    private void OnMouseOver()
    {
        //parentGrid.setLocalMouseX(x_ind);
        //parentGrid.setLocalMouseY(y_ind);

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Right Clicked {name}");
            if (objectInTile != null && !drag)
            {
                Destroy(objectInTile);
                objectInTile = null;
            }
        }
    }

    public GameObject getObjectInTile() { return objectInTile; }
}
