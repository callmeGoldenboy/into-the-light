using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootLightBalls : MonoBehaviour
{
    public GameObject lightBall;
    public int NumberOfBalls = 6;
	
    public float Strength = 3;
    public float IntervalSeconds = 3f;
	public float startDelay = 0f;					//Tillagd av David. Fördjöjer första skottet med värdet i sekunder.
    private float relativeTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        shoot();
    }

    public void shoot()
    {
        Vector3 shooterPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        //shooterPos.y -= 1;
        Vector3 direction = transform.up * -1;
        float offsetAngle = Mathf.Deg2Rad * (360 / NumberOfBalls);
        
        for(int i = 0; i < NumberOfBalls; i++)
        {
            direction = new Vector3(direction.x * Mathf.Cos(offsetAngle) + direction.y * Mathf.Sin(offsetAngle), -1 * direction.x * Mathf.Sin(offsetAngle) + direction.y * Mathf.Cos(offsetAngle));
            //direction = new Vector3(Mathf.Cos(offsetAngle), Mathf.Sin(offsetAngle));
            
           
            GameObject ball = GameObject.Instantiate(lightBall,new Vector3(shooterPos.x,shooterPos.y,0),new Quaternion());
            SlingshotLightBall slb = ball.GetComponent<SlingshotLightBall>();
            slb.Slingshot(direction, Strength);
            //offsetAngle += offsetAngle;// + Random.Range(0,5);
        }
        relativeTimer -= startDelay;
    }
    // Update is called once per frame
    void Update()
    {
        relativeTimer += Time.deltaTime;
        if(relativeTimer > IntervalSeconds)
        {
            shoot();
            relativeTimer = 0;
        }
    }
}
