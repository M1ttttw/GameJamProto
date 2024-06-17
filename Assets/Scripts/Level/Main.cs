using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameEvent startEvent;
    public GameEvent placementComplete;
    private GameEventListener placementCompleteListener;
    public GameEvent SceneLoaded;
    public bool rangeVisible;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        placementCompleteListener = this.gameObject.GetComponent<GameEventListener>();
        placementComplete.AddListener(placementCompleteListener);
        // SceneLoaded.TriggerEvent();
    }

    public void StartLevel(){
        StartCoroutine(SampleLevel());
    }
    IEnumerator SampleLevel()
    {
        while (true){
            yield return new WaitForSeconds(1f);
            Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-6f,18f), -1, 0), Quaternion.identity);
        }
    }
}
