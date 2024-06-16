using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class RangeBehavior : MonoBehaviour
{
    public GameObject parentVehicle;
    protected String opponentTag;
    protected Vehicle vehicleScript;
    protected List<TargetAndDistance> targets = new List<TargetAndDistance>();
    private GameObject prevTarget;
    private EnemyMovement em;
    private GameObject prevCCar;
    // Start is called before the first frame update
    void Start()
    {
        vehicleScript = parentVehicle.GetComponent<Vehicle>();
        if (parentVehicle.tag == "Player") { opponentTag = "Enemy"; }
        else { opponentTag = "Player"; }
        em = this.gameObject.transform.parent.gameObject.GetComponent<EnemyMovement>();
    }

    // Update itself
    void Update()
    {
        // Check for non-empty targets list.
        if (targets.Count > 0)
        {
            // Recalculate each target's distance, since it may have changed.
            for (int i = 0; i < targets.Count; i++) { targets[i].d = Vector3.Distance(parentVehicle.transform.position, targets[i].t.transform.position); }

            // Then just grab the target that has the smallest distance
            TargetAndDistance nearestOpp = targets[0];
            for (int i = 0;i < targets.Count; i++)
            {
                if (targets[i].d < nearestOpp.d) {
                    nearestOpp = targets[i];               
                }
            }

            prevTarget = nearestOpp.t;
            // Next we fire a bullet at the object that is closest in our targets
            if (vehicleScript.getTime() < vehicleScript.attackSpeed)
            {
                vehicleScript.incrementTime();
            }
            else
            {
                Debug.Log($"{parentVehicle.name} firing at {nearestOpp.t.name}: {nearestOpp.t.transform.position.x}, {nearestOpp.t.transform.position.y}, {nearestOpp.t.transform.position.z}");
                vehicleScript.attack(nearestOpp.t);
                vehicleScript.setTime(0);
            }
        }
        if (targets.Count == 0 && prevTarget is not null && GameObject.Find("Cars").transform.childCount>0){
            if (!System.Object.ReferenceEquals(em.closestCar,prevCCar) || (em.closestCar == null && prevCCar == null)){
                Debug.Log("swapped");
                
                em.reTarget();
            }
            prevCCar = em.closestCar;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {//Get tag of parentVehicle and change tag under based on parent tag
        if (other.gameObject.tag.Equals(opponentTag))
        {
            // Add this gameObject to the target's list
            TargetAndDistance new_entry = new TargetAndDistance(other.gameObject, Vector3.Distance(parentVehicle.transform.position, other.gameObject.transform.position));

            // Append it to the back this time.
            targets.Append(new_entry);

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
        if (other.gameObject.tag.Equals(opponentTag))
        {
            // Find the object in our targets and remove it.
            int i = 0;
            while (i < targets.Count && !GameObject.ReferenceEquals(other.gameObject, targets[i].t)) i++;

            targets.RemoveAt(i);

            Debug.Log($"Removed {other.name} from {parentVehicle.name} targeting");
        }
    }


}
