using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*README
Description: 
This is a trigger for restartLevel script

Public variables:
restartLevel -> must be an object that has the restartLevel script!


Dependencies: 
No dependencies.

How to use:
Drag and drop on suitable object, then drag object with restartLevel script in variable */

public class TriggerRestart : MonoBehaviour
{
    public restartLevel rlScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rlScript.restartScene();
            Debug.Log("Scene restart!");
        }
    }
}
