using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject stage;
    private List<GameObject> stageClones;

    public float spawnFrequency = 3;
    public float spawnLifetime = 5;
    public float scrollSpeed = 15;
    public float spawnOffset = 30;
    public Transform stageParent;

    private float spawnTimer = 0;
    private float spawnLifeTimer = 0;
    
    void Start()
    {
        stageClones = new List<GameObject>();
        GameObject stageClone = Instantiate(stage, transform.position, Quaternion.identity, stageParent);
        stageClone.GetComponent<StageMovement>().stageSpeed = scrollSpeed;
        stageClones.Add(stageClone);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnFrequency)
        {
            spawnTimer += Time.deltaTime;
        }
        else {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + spawnOffset, transform.position.z);
            GameObject stageClone = Instantiate(stage, pos, Quaternion.identity, stageParent);
            stageClone.GetComponent<StageMovement>().stageSpeed = scrollSpeed;
            stageClones.Add(stageClone);
            spawnTimer = 0;
        }

        if (spawnLifeTimer < spawnLifetime)
        {
            spawnLifeTimer += Time.deltaTime;
        }
        else {
            if (stageClones.Count > 0) {
                GameObject firstClone = stageClones.ElementAt(0);
                stageClones.RemoveAt(0);
                Destroy(firstClone);
            }
            spawnLifeTimer = 0;        
        }
    }
}
