using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
    public Transform parent; // Either cars or enemies
    public GameObject prefab;

    public void spawnPrefab() {
        Instantiate(prefab, transform.position, Quaternion.identity, parent);
    }
}
