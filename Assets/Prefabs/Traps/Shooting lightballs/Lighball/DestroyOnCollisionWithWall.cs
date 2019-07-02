using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionWithWall : MonoBehaviour
{   
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {

            PlayerKill pk = collider.GetComponent<PlayerKill>();//Get collider(The player object) playerkill script
            pk.kill(false); //Kill!

        }
        else if (collider.tag == "Ground")
        {
            Destroy(gameObject);//DestroyMe
        }
    }
    void Update() { }
}
