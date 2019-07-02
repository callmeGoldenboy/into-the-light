using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : MonoBehaviour
{
    public GameObject dyingLight;
    public restartLevel LevelRestarterScript;
    private Vector3 lightposition;
    // Start is called before the first frame update
    void Start()
    {
         
         lightposition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -30);
        
    }

    private bool wait = false;
    private float count = 0f;
    void Update()
    {
        if (!wait) { return; }
        else if((count += Time.deltaTime) >= 3f) {
            //GameObject light = GameObject.Instantiate(dyingLight, lightposition, new Quaternion());
            LevelRestarterScript.restartScene();}
    }
    
    public void kill(bool isDeathWall)
    {
        if (isDeathWall)
        {
            gameObject.GetComponentInChildren<playerSoundManager>().deathByWallb = true;
        }
        else
        {
            gameObject.GetComponentInChildren<playerSoundManager>().deathByTrapb = true;
           
        }
        
        wait = true;
        lightposition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -30);
        GameObject.Instantiate(dyingLight, new Vector3(lightposition.x,lightposition.y,0), new Quaternion());
        //gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        gameObject.GetComponentInChildren<Light>().enabled = false;
        foreach(BoxCollider2D BC in gameObject.GetComponents<BoxCollider2D>()) { BC.enabled = false; }
        
        
        gameObject.GetComponent<corpseScript>().collapse = true;
        gameObject.transform.position -= new Vector3(0, 0, 11);
        Debug.Log("killed");
    }
}
