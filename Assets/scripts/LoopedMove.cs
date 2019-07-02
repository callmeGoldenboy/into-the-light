/* README
 * LOOPED MOVE - BESKRIVNING
 * Det kopplade objektet rör sig enligt en givet bana beskrivet med en serie positioner.
 * Kombinera inte med rigidBody, eller något annat som skulle störa dess bana.
 * Notera att objektets ursprungsposition alltid ingår som första positionen i banan.
 *
 * MOVE MODE: Bestämmer objektets beteende efter att sista positionen har nåtts.
 * ========================
 * Välj mellan Cycle, där objektet börjar röra sig mot första positionen och sedan repeterar banan,
 * ------------------------
 * eller Backtrack, där objektet istället backar igenom alla tidigare positioner tills den når den första där den börjar om.
 *
 * POSITION MODE: Bestämmer hur positionerna beskrivs av developern.
 * ========================
 * Välj mellan Coordinates, där rena Unity-koordinater anges,
 * ------------------------
 * eller MoveVector, där man istället beskriver en vektor för förändringen i koordinater för att nå nästa position.
 *
 * POINTS OR VECTORS: SIZE
 * ========================
 * Ange hur många punkter utöver ursprungspunkten objektet ska röra sig mellan.
 * När ett värde (större än 0) har angets, bör en lista av lika många X-Y-par dyka upp, fyll i dessa för att markera positioner.
 * Kom ihåg att de tal du anger tolkas olika beroende på om du valde COORDINATES eller MOVEVECTOR som POSITION MODE.
 *
 * VELOCITY IN UNITS PER SECOND
 * ========================
 * Exakt vad som står, avgör hur snabbt objektet förflyttar sig, angett i Unity-enheter per sekund.
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopedMove : MonoBehaviour
{
    public enum movMod : byte {Cycle, Backtrack};               //[CYCLE] After reaching the final point, the object will move toward the first again.
    public movMod moveMode;                                     //[BACKTRACK] After reaching the final point, the object will move back through the list in reverse order.
    public enum posMod : byte {Coordinates, MoveVector};        //[COORDINATES] Points are assigned by their Unity world position.
    public posMod positionMode;                                 //[MOVEVECTOR] Points are described the the difference in position compared to the previous point.
    public Vector2[] pointsOrVectors;                           //Input array for the points to move to (EXCLUDING origianl position), or the vectors to move along.
    public float velocityInUnitsPerSecond;                      //Velocity of the moving object
    private Transform stickyTransform = null;                   //Transform component of the player, assigned in the StickySrface code.

    private Vector2[] targetPoints;                             //Private array of points that the object should visit, including the point it starts at.
    private Vector2[] moveVectors;                              //Private array of vectors that hold the move vectors toward the next targetpoint, set up during Start()
    //private Vector2 origin;                                     //Private vector that saves the original position, assumed z = 0.
    private short target = 1;                                   //Private value that holds the index of the current target point.
    private short vector = 0;                                   //Private value that holds the index of the current move vector.
    private bool faulty = false;                                //Private bool, set to true if no Points are assigned. 
    private Vector2 direction;                                  //Private direction vector, points toward next point, should be a unit vector (replace with move vectors?)
    private double mileage = 0;                                 //Private value keeping track of how far an object moves each frame. NOT A MEASUREMENT OF MAGNITUDE. (X+Y)
    private bool reversing = false;								//Private bool that signals if the next movement should be reversed or not, only used with backtracking.

    // Start is called before the first frame update
    void Start()
    {
        //CALCULATE ARRAY SIZES
        targetPoints = new Vector2[pointsOrVectors.Length + 1];                                         //Not sensitive to coordinate/vector option
        if (moveMode == movMod.Cycle) moveVectors = new Vector2[pointsOrVectors.Length + 1];            //Cycle case
        else moveVectors = new Vector2[pointsOrVectors.Length * 2];                                     //Backtrack case

        if (pointsOrVectors.Length <= 0) { faulty = true; Debug.Log("PLEASE ASSIGN MOVEMENT POINTS OR VECTORS FOR LOOPEDMOVE SCRIPT ON " + this); }  //Array too short.
        else if (positionMode == posMod.Coordinates)                                                                    //Input given as coordinates.
        {
            targetPoints[0] = (Vector2)this.GetComponent<Transform>().localPosition;                                    //Record original position.       
            int i;                                                                                                      //Declare counter
            for (i = 0; i < pointsOrVectors.Length; i++) targetPoints[i + 1] = pointsOrVectors[i];                      //Copy over remaining            
            for (i = 0; i < targetPoints.Length - 1; i++) moveVectors[i] = targetPoints[i + 1] - targetPoints[i];       //Calculate move vectors            
            if (moveMode == movMod.Cycle) moveVectors[i] = targetPoints[0] - targetPoints[i];                           //Cycle case: Add returning move vector                        
            else for (int j = 0; i + j < moveVectors.Length; j++) moveVectors[i + j] = (-1) * moveVectors[i - (j + 1)]; //Backtrack case: Add all the backtracking move vectors
            direction = moveVectors[0] / moveVectors[0].magnitude;                                                      //Calculate direction vector for first movement (unit)
        }
        else                                                                                                            //Input given as move vectors.
        {
            targetPoints[0] = (Vector2)this.GetComponent<Transform>().localPosition;                                    //Record original position.
            int i;
            for (i = 0; i < pointsOrVectors.Length; i++) moveVectors[i] = pointsOrVectors[i];                           //Copy over vectors           
            for (i = 1; i < targetPoints.Length; i++) targetPoints[i] = targetPoints[i - 1] + moveVectors[i - 1];       //Calculate remaining positions
            i--;                                                                                                        //Decrement i (because reasons)
            if (moveMode == movMod.Cycle) moveVectors[i] = targetPoints[0] - targetPoints[i];                           //Cycle case: Add returning move vector                        
            else for (int j = 0; i + j < moveVectors.Length; j++) moveVectors[i + j] = (-1) * moveVectors[i - (j + 1)]; //Backtrack case: Add all the backtracking move vectors
            direction = moveVectors[0] / moveVectors[0].magnitude;                                                      //Calculate direction vector for first movement (unit)
        }
		
		if(moveMode == movMod.Backtrack && targetPoints.Length == 2) reversing = true;									//Bug fix in case of instant reverse.
    }

    void Update()
    {      
        //SKIP EVERYTHING IF FAULTY INPUT (FAULTY == NO TARGET POINTS)
        if(!faulty)
        {
            //Debug.Log(direction.magnitude - 1.0);
            Vector2 step = Time.deltaTime * velocityInUnitsPerSecond * direction;       //Calculate the next step.
            this.GetComponent<Transform>().localPosition += (Vector3)step;              //Move Object along with the calculated step direction vector, toward the next target.
            if(stickyTransform != null)                                                 //If a player has been registered on top of this object.
                stickyTransform.position += (Vector3)step;                              	//Also move the player on top.
            mileage += Mathf.Abs(step.x) + Mathf.Abs(step.y);                           //Record how far the step took the object along axis X and axis Y. (summed)
            int i = 0;
            while(i++ < 100)
            {
                if (mileage >= Mathf.Abs(moveVectors[vector].x) + Mathf.Abs(moveVectors[vector].y))                     		//Has the target been reached? If so, compute a bunch of things.
                {
                    //Debug.Log("TARGET REACHED");
                    mileage = Vector2.Distance((Vector2)this.GetComponent<Transform>().localPosition, targetPoints[target]);    //Calculates the distance covered beyond the target.
                    this.GetComponent<Transform>().localPosition = (Vector3)targetPoints[target];                               //Corrects position.
                    vector = (short) ((vector + 1) % moveVectors.Length);                                                       //Increments current index of the moveVector array.
                    target = nextTarget(target, vector);                                                                        //Calculates next index of the targetPoints array.
                    direction = moveVectors[vector] / moveVectors[vector].magnitude;                                            //Calcuates a new unit vector, pointing toward the new target.
                    step = direction * (float)mileage;                                                                          //Calculate the excess step.
                    mileage = Mathf.Abs(step.x) + Mathf.Abs(step.y);                                                            //Calculate new mileage.
                    this.GetComponent<Transform>().localPosition += (Vector3)step;                                              //Adds the excess distance covered toward the next target.
                    if (stickyTransform != null)                                                                                //If a player has been registered on top of this object.
                        stickyTransform.position += (Vector3)step;                                                              	//Also move the player on top.
                }
                else break;
            }
            if (i == 100) Debug.LogWarning("Potentially infinite loop aborted, this is probably not supposed to happen, look into it!");
        }
    }

    private short nextTarget(short target, short vector)
    {
        if (moveMode == movMod.Cycle) return (short)((target + 1) % targetPoints.Length);       //Cycle mode, simple increment with modulus.
        else if (!reversing)                                                                    //Backtrack mode, moving forward.
        {
            target += 1;
            if(target == targetPoints.Length - 1) reversing = true;
            return target;
        }
        else                                                                                    //Backtrack mode, moving backwards.
        {
            target -= 1;
            if (target == 0) reversing = false;
            return target;
        }
    }

    public void Stick (Transform transform)														//Called by player's scripr STICKY SURFACE when landing on this object.
    {
        this.stickyTransform = transform;
    }
}
