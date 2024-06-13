using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBehavior : MonoBehaviour
{
    public GameObject parentVehicle;
    protected Vehicle vehicleScript;

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = parentVehicle.GetComponent<Vehicle>();
    }


    // Update itself
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log($"Firing at {other.gameObject.transform.position.x}, {other.gameObject.transform.position.y}, {other.gameObject.transform.position.z}");
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.gameObject.tag.Equals("Enemy"))
        {
            if (vehicleScript.getTime() < vehicleScript.attackSpeed)
            {
                vehicleScript.incrementTime();
            }
            else
            {
                Debug.Log($"Firing at {other.gameObject.transform.position.x}, {other.gameObject.transform.position.y}, {other.gameObject.transform.position.z}");
                vehicleScript.setTime(0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        vehicleScript.setTime(0);
    }


}
