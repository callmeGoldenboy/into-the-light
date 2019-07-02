/* README
 * STICKY SURFACE - BESKRIVNING
 * Detta skript ska anslutas till Spelarens GROUND CHECK objekt.
 * Kod som ser till att om spelaren står på en plattform i rörelse p.g.a LOOPED MOVE skriptet,
 * så ska spelaren följa dess rörelse längs X- och Y-axeln tills att spelaren hoppar eller faller av plattformen.
 * 
 * PLAYER
 * ===============================
 * Referens till spelarens avatar, som ska fastna på ytan.
 * 
 * OBS!
 * ===============================
 * Denna kod är inte generellt applicerbar. T.ex. har skriptet LOOPED MOVE endast plats för ett objekt som den kan flytta omkring.
 * SÄG TILL MIG (DAVID) OM DET FINNS BEHOV ATT UTÖKA FUNKTIONALITETEN. 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickySurface : MonoBehaviour
{

    public GameObject player;                               //Reference to player object.
    private Transform pos;                                  //Private reference to the player's Transform component.

    // Start is called before the first frame update
    void Start()
    {
        pos = player.GetComponent<Transform>();             //Assign pos variable.
    }

    // Update is called once per frame
    void Update()
    {

    }

    //When the player lands on a sticky object, STICK!
    private void OnCollisionEnter2D(Collision2D collision)          //If a collision occurs...
    {
        //Debug.Log("Collision!");
        if (collision.collider.tag == "Ground")                         //... and if the other object is tagged as Ground...
        {
            //Debug.Log("Ground collision!");
            if (collision.collider.GetComponent<LoopedMove>() != null)      //... and if it also has the LOOPED MOVE script...
            {
                //Debug.Log("Mobile collision!");
                collision.collider.GetComponent<LoopedMove>().Stick(pos);       //...then give that object the player's transform so it can be manipulated.
            }
        }
    }

    //When the player steps/jumps off a sticky object, RELEASE!
    private void OnCollisionExit2D(Collision2D collision)          //If a collision occurs...
    {
        //Debug.Log("Exit!");
        if (collision.collider.tag == "Ground")                         //... and if the other object is tagged as Ground...
        {
            //Debug.Log("Ground exit!");
            if (collision.collider.GetComponent<LoopedMove>() != null)      //... and if it also has the LOOPED MOVE script...
            {
                //Debug.Log("Mobile exit!");
                collision.collider.GetComponent<LoopedMove>().Stick(null);       //...then give that object the player's transform so it can be manipulated.
            }
        }
    }
}
