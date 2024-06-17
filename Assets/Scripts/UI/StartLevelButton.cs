using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelButton : MonoBehaviour
{
    public GameEvent startLevel;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startLevelEvent);
    }

    void startLevelEvent(){
        Debug.Log("trigger");
        startLevel.TriggerEvent();
    }
}
