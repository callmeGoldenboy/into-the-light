using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*README
 Description: 
 This script will activate some trap when it the gameobject collides with the player

Public variables:
TrapToBeAcivated -> Must be some object(trap) that has a trap script as component!


Dependencies: 
TrapToBeActivated must have trap script.
This script works only when the gameobject has some kind of boxcollider!

How to use:
Drag on some object that have some kind of collider component (like boxcollider)  */

public class platformTrigger : MonoBehaviour
{
    public Trap TrapToBeActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("Triggered!");
            TrapToBeActivated.activate();
        }
    }
}
