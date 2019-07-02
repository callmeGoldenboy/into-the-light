using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsWall : MonoBehaviour
{
    public Rigidbody2D body2d;
    public float leftRange;
    public float rightRange;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = speed;


    }

    // Update is called once per frame
    void Update()
    {
        swing();
    }

    void swing()
    {
        if (transform.rotation.z > 0 && 
            transform.rotation.z <rightRange
            && (body2d.angularVelocity >0) && 
            body2d.angularVelocity < speed)
        {
            body2d.angularVelocity = speed; 
        }
        else if (transform.rotation.z < 0 &&
            transform.rotation.z < leftRange
            && (body2d.angularVelocity < 0) &&
            body2d.angularVelocity > speed * -1)
        {
            body2d.angularVelocity = speed * -1;
        }


       

    }
}

