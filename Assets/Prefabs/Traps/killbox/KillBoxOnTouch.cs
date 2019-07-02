
using UnityEngine;

public class KillBoxOnTouch : MonoBehaviour
{   
    public bool isDeathWall = false;
    //check if it collides with the player 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            
            PlayerKill pk = collider.GetComponent<PlayerKill>();//Get collider(The player object) playerkill script
            pk.kill(isDeathWall); //Kill!

            //Debug.Log("killed!");

        }

        
    }
}
