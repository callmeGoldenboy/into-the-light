using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*README
 Description: 
 This trap script will make any gameobject with rigidbody fall when called the function activate()

Public variables:
rb -> The rigidbody2d it will make fall
fallingSpeed -> how fast it will fall


Dependencies: 
Trap must have RigidBody2D component with kinematic!!!!

How to use:
Drag on to trap! Drag the RigidBody2D component into the variable on script. (Must be triggered with trigger)*/

public class TrapFalling : Trap
{
    public Rigidbody2D rb;
    public float fallingSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void activate()
    {
        rb.gravityScale = fallingSpeed;
        rb.isKinematic = false;
    }
}
