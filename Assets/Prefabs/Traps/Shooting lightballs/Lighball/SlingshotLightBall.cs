using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotLightBall : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 dir;
    private float speed;
    public void Slingshot(Vector3 direction,float speed)
    {
        isMoving = true;
        dir = direction;
        this.speed = speed;
        move();
    }
    private void move()
    {
        
        gameObject.transform.position += dir.normalized * Time.deltaTime * speed;
        
    }
    void Update()
    {
        if (isMoving)
        {
            move();
        }
    }
}
