using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TileInteractable : MonoBehaviour
{
    Renderer rend;

    public Material[] mat_arr = new Material[3];
    public int selector = 0;

    public GridManager parentGrid;
    public GameObject spawnObject;
    private GameObject objectInTile;

    public int x_ind;
    public int y_ind;

    private bool drag = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting Transparent");
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        selector = 0;
        rend.sharedMaterial = mat_arr[selector];
    }

    // Update is called once per frame
    void Update()
    {
        rend.sharedMaterial = mat_arr[selector];
        if (objectInTile != null && !drag)
        {
            objectInTile.transform.position = transform.position;
        }
    }
    private void OnMouseEnter()
    {
        Debug.Log($"Mouse has entered on {this.gameObject.name}");
        selector = 1;
        spawnObject = parentGrid.spawnSharedObject;
    }

    private void OnMouseExit()
    {
        Debug.Log($"Mouse has exited on {this.gameObject.name}");
        selector = 0;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Mouse has clicked on {this.gameObject.name}");
        selector = 2;
        if (objectInTile == null)
        {
            Vector3 clickPos = new Vector3(transform.position.x, transform.position.y, -5);
            objectInTile = Instantiate(spawnObject, clickPos, Quaternion.identity, parentGrid.spawnUnderThisObject);
        }
    }

    private void OnMouseUp()
    {
        Debug.Log($"Mouse has been released on {this.gameObject.name}");
        if (drag)
        {
            selector = 0;

            if (objectInTile != null)
            {
                int currMouseX = parentGrid.getLocalMouseX();
                int currMouseY = parentGrid.getLocalMouseY();

                if (!(currMouseX == x_ind && currMouseY == y_ind))
                {
                    // Mouse was released on a location that is not this tile.
                    TileInteractable released_tile = parentGrid.getTileInteractable()[currMouseX, currMouseY];

                    if (released_tile.objectInTile == null)
                    {
                        // Move the object here.
                        released_tile.objectInTile = objectInTile;
                        objectInTile = null;

                        Debug.Log("Moved object");
                    }
                    else
                    {
                        // Otherwise swap the objects
                        GameObject temp1 = objectInTile;
                        GameObject temp2 = released_tile.objectInTile;

                        released_tile.objectInTile = temp1;
                        objectInTile = temp2;

                        Debug.Log("Swapped Object");
                    }
                }
            }

        }
        else
        {
            selector = 1;
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
        parentGrid.setLocalMouseX(x_ind);
        parentGrid.setLocalMouseY(y_ind);
    }
}
