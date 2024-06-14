using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;





public class RangeBehavior : MonoBehaviour
{
    public GameObject parentVehicle;
    protected Vehicle vehicleScript;
    protected List<TargetAndDistance> targets = new List<TargetAndDistance>();

    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = parentVehicle.GetComponent<Vehicle>();
    }

    // Update itself
    void Update()
    {
        // Check for non-empty targets list.
        if (targets.Count > 0)
        {
            // Recalculate each target's distance, since it may have changed.
            for (int i = 0; i < targets.Count; i++) { targets[i].d = Vector3.Distance(parentVehicle.transform.position, targets[i].t.transform.position); }

            // Then sort the targets by each target's new distance. Recall: targets is a list of TargetAndDistance objects
            targets.Sort((x, y) => x.d.CompareTo(y.d));

            // Next we fire a bullet at the first item (the object that is closest) in our targets
            if (vehicleScript.getTime() < vehicleScript.attackSpeed)
            {
                vehicleScript.incrementTime();
            }
            else
            {
                Debug.Log($"{parentVehicle.name} firing at {targets[0].t.name}: {targets[0].t.transform.position.x}, {targets[0].t.transform.position.y}, {targets[0].t.transform.position.z}");
                vehicleScript.attack(targets[0].t);
                vehicleScript.setTime(0);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            // Add this gameObject to the target's list
            TargetAndDistance new_entry = new TargetAndDistance(other.gameObject, Vector3.Distance(parentVehicle.transform.position, other.gameObject.transform.position));

            // You may assume that targets is sorted... because it's being sorted in the Update method. This assumption will obviously lead to some optimization improvements
            int i = 0;
            while (i < targets.Count && new_entry.d > targets[i].d) i++;
            targets.Insert(i, new_entry);

            Debug.Log($"Added {other.name} to {parentVehicle.name} targeting");

            // We also want to fire a bullet straight away when we have 1 target in our range.
            if (targets.Count == 1) {
                Debug.Log($"{parentVehicle.name} firing at {other.gameObject.name}: {other.gameObject.transform.position.x}, {other.gameObject.transform.position.y}, {other.gameObject.transform.position.z}");
                vehicleScript.attack(other.gameObject);
                vehicleScript.setTime(0);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            // Find the object in our targets and remove it.
            int i = 0;
            while (i < targets.Count && !GameObject.ReferenceEquals(other.gameObject, targets[i].t)) i++;

            targets.RemoveAt(i);

            Debug.Log($"Removed {other.name} from {parentVehicle.name} targeting");
        }
    }


}
