using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMovingSpikeWall : Trap
{
    // Start is called before the first frame update
    public LoopedMove move;
    void Start()
    {
        move = gameObject.GetComponent<LoopedMove>();
        move.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void activate()
    {
        move.enabled = true;
        
    }
}
