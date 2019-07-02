/* README
 * OVAL MOVE - BESKRIVNING
 * ===================================
 * Detta skript är avsett exclusivt för armarna i dödsväggen, den hanterar 
 * armarnas cirkulära rörelse och rotationer något slumpmässigt, så att man 
 * sällan ser två armar som beter sig likadant. Enda in-datan som krävs är 
 * en relativ vektor från bildens centrum till punkten på bilden som bör vara 
 * statisk. Justera denna vektor varje gång en ny bild används med skriptet.
	Oval Move vänder också på en slumpmässig hälft av bilderna upp och ned.
 * 
 * ARM ROOT
 * ===================================
 * Lokala koordinaterna för punkten armen bör rotera runt. Tanken här att det
 * ska se ut som om de böjer sig upp och ned. I koden kommenteras denna
 * rörelse därför som "Bend", trots att bilden själv inte böjer sig.
 * 
 * 
 * PLAYER - !LÄMNA TOM!
 * ===================================
 * Referens till spelaren's avatar. Denna behöver inte ställas in manuellt, det hanteras
 * av skriptet PlayerReference.cs som bör sitta på dödsväggen.
 * 
 * INTENSIFY THRESHOLD
 * ===================================
 * Avståndet till spelaren's avatar inom vilket detta objekt bör bli mer intensivt.
 * 
 * 
 * SPEED INTENSITY
 * ===================================
 * Hur mycket mer intensivt detta objekt bör bli som mest vad gäller hastighet. 
 * Intensiviteten stiger linjärt beroende på avståndet inom thresholden.
 * 
 * SCALE INTENSITY
 * ===================================
 * Hur mycket mer intensivt detta objekt bör bli som mest vad gäller skala. 
 * Intensiviteten stiger linjärt beroende på avståndet inom thresholden.
 * 
 * USE
 * ===================================
 * Använd inom ShadeWall prefaben, i kombination med LOOPED MOVE och WALL MOVE. 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalMove : MonoBehaviour
{
    public float intensifyThreshold = 3f;                           //Distance from player at which, this object starts to intensify.
    public float speedIntensity = 2f;                               //Maximum Factor to intensify by (one less than really) in speed.
    public float scaleIntensity = 2f;                               //Maximum Factor to intensify by (one less than really) in scale.

    //VARIABLES INITIALIZED WITH RANDOMIZED VALUES DURING START()
    private float verticalModifier;                                 //Vertical max distance multiplier from centre.
    private float horizontalModifier;                               //Max distance a Shand can move from its parent object horizontally.
    private float startAngleDegrees;                                //Starting angle, measured in degrees.
    private float rotationsPerSecond;                               //Speed measured in full rotations per second.

    
    private float bendUpperLimit;                                   //Restricts this object's bending (positive) in degrees.
    private float bendLowerLimit;                                   //Restricts this object's bending (negative) in degrees.
    private float bendSpeed;                                        //Speed at which this object bends in degrees per second.
    private float startBend;                                        //Starting rotation this object should have.
    
    // END OF RANDOMIZED VARIABLES

    private Transform t;                                            //Holds reference to this objects transform component.

    private float currentAngle;									    //Records the current angle. Initially equal to startAngleDegrees.
	private float degSpeed;								    		//rotationsPerSecond speed value translated into degrees per second.
	//private float zPos;									            //Saves the z-value of the objects position.
    private Vector3 startScale;                                     //Records the initial scale, so it can be reset.
    private float startSpeed;                                       //Records the initial speed, so it can be reset.
    private GameObject player;                                      //Reference to player, assigned by PlayerReference.cs.
    
    //Property used by PlayerReference.cs, hands off!
    public void SetPlayer(GameObject player) { this.player = player; }

    // Start is called before the first frame update
    void Start()
    {
        t = this.GetComponent<Transform>();                                 //Assign proper transform component to variable.
        Vector2 handScale = (Vector2)t.GetChild(0).localScale;

        //Randomize some values!
        verticalModifier = Random.Range(0f, 5f) * handScale.y;              //Randomize and multiply by scale to calculate modifier.
        horizontalModifier = Random.Range(1f, 3f) * handScale.x;            //Randomize and multiply by scale to calculate modifier.

        startAngleDegrees = Random.Range(0f, 360f);                         //Randomize Starting angle.
        rotationsPerSecond = Random.Range(0.1f, 0.25f);                     //Randomize Rotation speed.
        degSpeed = rotationsPerSecond * 360f;								//Redefine rotations speed as degrees per second.
        if (1 == (short)(Random.value + 0.5f)) degSpeed *= -1;              //Flip a data-coin for clockwise/counter-clockwise.
                
        if (1 == (short)(Random.value + 0.5f))                              //Flip a data-coin, if heads...
        {
            Vector3 vec = t.localScale;                                         //...get scale.
            vec.y *= -1;                                                        //Invert y-scale.
            t.localScale = vec;                                                 //Save new scale.

            vec = t.localRotation.eulerAngles;                                  //Get current rotation. (should be pointing right)
            vec.z *= -1;                                                        //mirror rotation along x-axis.
            t.rotation = Quaternion.Euler(vec.x, vec.y, vec.z);                 //Save new rotation.
        }
                
        bendUpperLimit = Random.Range(10f, 60f);                            //Randomize upper bend limiter.
        bendLowerLimit = Random.Range(10f, -60f);                           //Randomize lower bend limiter.
        bendSpeed = Random.Range(10f/1.5f, 45f/1.5f);                       //Randomize bending speed.
        if (1 == (short)(Random.value + 0.5f))                              //Flip a data-coin, if heads...
            bendSpeed *= -1;                                                    //Set bending speed to negative.
        startBend = Random.Range(bendLowerLimit, bendUpperLimit);           //Randomize starting bend somewhere between limits.
        t.Rotate(Vector3.forward, startBend);                               //Rotate this object to its randomized value.
        bendUpperLimit -= startBend;                                        //Adjust limiters so they can be used...
        bendLowerLimit -= startBend;                                        //...as counters from here on out.
        
		currentAngle = startAngleDegrees;									//Initialize the current angle as the starting angle.
        startSpeed = degSpeed;                                              //Initialize the starting speed record.
        startScale = t.localScale;                                          //Initialize the starting scale record.

		Move();																//Move object to its starting point.
    }

    // Update is called once per frame
    void Update()
    {
        float timeDiff = Time.deltaTime;                    //Save the time difference right away, saves some characters.

        //Update angle
        currentAngle += degSpeed * timeDiff;                //Increace angle by the speed value based on time difference.

        //Modulus angle
        currentAngle %= 360f;                               //Restrict Angle between 0 and 360 degrees, keep it simple.

		//Update position
		Move();                                             //Refresh position by calling the Move function.

        //Intensify hand
        if(player != null)                                  //If player avatar is in play...
            Intensify();                                        //...intensify speed and scale if close to player.

        Bend(bendSpeed * timeDiff);                         //Bend arm, depending on time passed and speed of bend.
    }

    //Updates Object position based on the currentAngle variable.
    private void Move()
	{
        float rads = currentAngle * Mathf.Deg2Rad;                          //Translate the current angle into radians
		t.localPosition =                                                   //Move this object to local coordinates...
			new Vector3(Mathf.Cos(rads) * horizontalModifier,                   //X = Cosine of angle times modifier.
			Mathf.Sin(rads) * verticalModifier, 0f);                          //Y = Sine of angle times modifer.
	}

    //Intensify speed and scale if close to player.
    private void Intensify()
    {
        Vector3 playerPos = new Vector3(player.GetComponent<Transform>().position.x,        //Apparently player's centre is at feet.     
            player.GetComponent<Transform>().position.y + 1.3f, 0);                             //So retrieve a modified position.
        float diff = (playerPos - t.position).sqrMagnitude;                                 //Calculate distance between player and this object. (squared)

        if (diff < intensifyThreshold * intensifyThreshold)                                 //If the player is closer than the threshold. (square threshold too)
        {
            diff = Mathf.Sqrt(diff);                                                        //Calculate root of distance, so it's correct.
            t.localScale = startScale * ((scaleIntensity *(intensifyThreshold - diff)       //Intensify the scale between...
                / intensifyThreshold) + 1);                                                     //...1 and [intensity] + 1.
            degSpeed = startSpeed * ((speedIntensity * (intensifyThreshold - diff)          //Intensify the speed between...
                / intensifyThreshold) + 1);                                                     //...1 and [intensity] + 1.
        }
        else                                                                                //Otherwise, if the player is too far, correct speed and scale.
        {
            t.localScale = startScale;                                                      //Correct scale.
            degSpeed = startSpeed;                                                          //Correct speed.
        }
    }

    //Bend arm
    private void Bend(float spin)
    {
        //Bend arm
        t.Rotate(Vector3.forward, spin);                    //Rotate the arm according to the input value.
        bendUpperLimit -= spin;                             //Update limiters.
        bendLowerLimit -= spin;                             //(counters at this point, really)

        //Check limiters
        if ((bendUpperLimit <= 0 && bendSpeed > 0) ||       //If upper limit has been passed and speed is positive...
            (bendLowerLimit >= 0 && bendSpeed < 0))             //...or if lower limit has been passed and speed is negative...
            bendSpeed *= -1;                                    //...invert the speed.
    }
}
