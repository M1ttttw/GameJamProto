using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour
{
    public float stageSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * stageSpeed * Time.deltaTime;
    }
}
