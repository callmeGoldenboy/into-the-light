/* README
 * WALL MOVE - BESKRIVNING
 * ====================
 * Koden är avsedd att sättas på dödsväggen. 
 * Algoritmen manipulerar obejktets position så att den rör sig mot en referenspunkt. (Spelaren)
 * Vanligtvis rör sig väggen med konstant hastighet, men skulle spelaren komma för långt bort börjar den kompensera.
 * Koden ser också till att hålla väggen på samma höjd (y-led) som kameran.
 * 
 * BASE SPEED
 * ====================
 * Väggens konstanta hastighet i Unity-enheter per sekunder.
 * 
 * BOOST THRESHOLD
 * ====================
 * Ett gränsvärde i Unity-enheter på hur långt framför objektet referensen får vara innan objektets hastighet bör öka.
 * 
 * DYNAMIC BOOST - Bestämmer vilken typ att hastighetsökning som bör ske om referensen hamnar bortom gränsvärdet.
 * ====================
 * FALSE: Detta objektets hastiget ökar med ett bestämt värde i Unity-enheter per sekund.
 * --------------------
 * TRUE: Detta objektets hastiget ökar med ett linjärt värde beroende på skillnaden mellan gränsvärdet och avståndet till referenspunkten.
 * 
 * BOOST - Värde som avgör hastighetsökningen beroende på alternativet innan.
 * ====================
 * Om DYNAMIC BOOST är FALSE så är detta värde hur många Unity-enheter per sekund extra objektet bör flytta på sig.
 * --------------------
 * Om DYNAMIC BOOST är TRUE så är detta värde hur många Unity-enheter per sekund snabbare objektet rör sig,
 * per 1 Unity-enhets avståndskillnad mellan gränsvärdet och referenspunkten.
 */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    public float baseSpeed = 1f;            //Constant speed value, moving along increasing x-axis value. (right)
    public float boostThreshold = 5f;       //Distance beyond which the wall should move even faster.
    public bool dynamicBoost = false;       //[False] Boost is applied as a constant value.
                                            //[True] Boost increases as the extra distance (beyond threshold) increases.
    public float boost = 2f;                //[Static Boost] velocity to be added to the base speed value.
                                            //[Dynamic Boost] factor to multiply the extra distance... (beyond threshold)
                                                //...with in order to calculate velocity added to the base speed value.
    private GameObject player;              //Player object that is to be chased by this wall.
    private Camera cam;                     //Reference to camera that determines the y-position of the wall.
    private float distance = 0;             //Value of distance between wall and player.

    public void SetPlayer(GameObject player) { this.player = player; }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;               //Initialize camera reference to the first enabled camera tagged with "MainCamera".
    }

    // Update is called once per frame
    void Update()
    {
        float yDiff = cam.transform.position.y - this.transform.position.y;          //Calculate y-difference so the wall is moved correctly along the y-axis.

        if (player != null) 
            distance = player.transform.position.x - this.transform.position.x;		//calculate distance
        else distance = 0;                                                          //If the player has been destroyed, stop boosting.

        if( distance > boostThreshold)    //Distance too great, engage boost.
        {
            if(dynamicBoost)        //Boost dynamically
            {
				transform.position += new Vector3((baseSpeed + (boost * (distance - boostThreshold))) * Time.deltaTime, yDiff, 0f);
            }
			else					//Boost statically
			{
				transform.position += new Vector3((baseSpeed + boost) * Time.deltaTime, yDiff, 0f);
			}
        }
		
		else 						//Distance is small, move at regular speed.
		{
			transform.position += new Vector3(baseSpeed * Time.deltaTime, yDiff, 0f);
		}
    }
}
