using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*README
 Description: 
 Time based trigger!

Public variables:
How many traps to activate must be defined, for one just write 1
TrapToBeActivated -> must be some trap that inherits (is a child of) the super class trap

Dependencies: 
That there is a trap to be activated

How to use:
Drag on any object as a component, and specify which trap to be activated*/

public class TimeBasedTrigger : MonoBehaviour
{
    public Trap[] TrapToBeActivated;
    public float TimeToActivate = 3f;

    private float relativeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        relativeTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        relativeTime += Time.deltaTime;
        if(relativeTime > TimeToActivate)
        {
            foreach(Trap t in TrapToBeActivated) {
                t.activate();
            }
        }
    }
}
