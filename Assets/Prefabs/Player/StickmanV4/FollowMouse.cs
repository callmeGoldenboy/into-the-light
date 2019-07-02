/* README
 * FOLLOW MOUSE - BESKRIVNING
 * Detta skript bör anslutas till spelarens ROTATE AROUND THIS objekt, ett barn till RIGHT ARM SOLVER, barn till STICKMAN.
 * Koden ser till att ROTATE AROUND THIS objektet och dess barn roterar i z-led relativt musens position, med avsikt att
 * få ficklampan och armen att peka mot musen. Om hamnar bakom spelarens avatar så kommer hela STICKMAN objektet vända på.
 * (genom att multiplicera scale.x med -1)
 * 
 * ROTATION SPEED
 * ===============================
 * Avgör hur snabbt detta objekt följer musen, med ett längre tal ökar fördröjningen, med ett högre tal minskar det.
 * 
 * PLAYER
 * ===============================
 * Referens till spelarens avatar, vars arm skall följa musen.
 */


using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public float rotationSpeed = 8f;						//How quickly, or "tightly" the object should rotate toward the mouse.
    public GameObject player;								//Reference to the player's avatar.

    private void Update()
    {
        Rotation();											//Rotate toward mouse.
        CheckFlip();										//Check if avatar needs to be flipped.

    }

	//Rotate object toward mouse.
    private void Rotation()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;			//Get directional vector.
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);									//Calculate the angle between the position of the mouse and the postition of the light.
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);										//Translate angle to quaternion from.
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,rotationSpeed * Time.deltaTime);		//Rotate the object using the quaternion.

      
    }
    
	//Check if avatar needs to be flipped.
    private void CheckFlip()
    {
        float rotationZ = this.gameObject.GetComponent<Transform>().eulerAngles.z;		//Get roation is degrees.
        bool facingLeft = player.GetComponent<PlayerMovement>().GetFacingLeft();		//Retrieve facing from player reference.

        if(facingLeft != ((rotationZ % 360) > 90 && (rotationZ % 360) < 270))			//If this object points in the opposite direction compared to the player avatar...
        {
            Flip(facingLeft);															//...flip the avatar.
        }
        
    }
	
	//Flip the avatar. Takes current facing as argument: Right -> FALSE, Left -> TRUE.
    private void Flip(bool facingLeft)
    {
        Vector3 newScale = transform.localScale;										//Retrieve this object's scale.
        newScale.x *= -1;																//Invert x-axis of scale.
		newScale.y *= -1;																//Invert y-axis of scale. (Alignment fix)
        transform.localScale = newScale;												//Update this object's scale.

        transform.localRotation = Quaternion.Inverse(transform.localRotation);			//Calculate the opposite rotation of the current rotation as quaternions.

        newScale = player.GetComponent<Transform>().localScale;							//Retrieve current scale of player.
        newScale.x *= -1;																//Invert x-axis of scale.
        player.GetComponent<Transform>().localScale = newScale;							//Update player's scale.
        player.GetComponent<PlayerMovement>().SetFacingLeft(!facingLeft);				//Update facing variable.
    }
    

}
