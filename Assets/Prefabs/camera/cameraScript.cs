/* README
 * CAMERASCRIPT - BESKRIVNING
 * Detta skript bör anslutas till kameran, det hanterar kamerans rörelse fullständigt.
 * Skriptet ser till att spelaren inte hamnar utanför bilden, och att ingen till vänster
 * om dödsväggen syns heller.
 * 
 * CONSTRAINTS
 * ==========================================
 * Bestämmer hur långt ifrån kameran spelaren får komma, i den antydda riktningen, innan kameran måste flyttas.
 * 
 * CAMERASPEED
 * ==========================================
 * Om kameran måste flytta på sig på grund av situationen ovan, flyttas kameran i den antydda riktningen med denna
 * hastighet, angivet i Unity-enheter/sekund
 * 
 * THE DEATH WALL
 * ==========================================
 * Referens till dödsväggen, detta behövs för att kameran ska kunna puttas av väggen.
 * 
 * PLAYER
 * ==========================================
 * Referens till spelarens avatar, kameran kommer att försöka följa det denna referens pekar till.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public float HorizontalConstraint = 2f;
    public float VerticalUpConstraint = 1.5f;
    public float VerticalDownConstraint = 1f;
    public float CameraSpeedForward = 4;
    public float CameraSpeedBackwards = 4;
    public float CameraSpeedDown = 3;
    public float CameraSpeedUp = 3;
	public float verticalOffset = 3f;

    public GameObject TheDeathWall;
    public GameObject Portal;
    public GameObject player;	

	
	private float DeathwallWidth = 0.6f;
    private bool PortalReached = false;
    private bool PortalAndDeathWallReached = false;

    // Start is called before the first frame update
    void Start()
    {
       DeathwallWidth *= TheDeathWall.transform.localScale.x/2;
     
    }
    void FixedUpdate()
    {
    //    Update();
    }
    // Update is called once per frame
    void Update()
    {   

        //if (PortalAndDeathWallReached) { return; }//Camera fixed

        Transform cameraTransform = this.GetComponent<Transform>();
        float halfScreenLength = this.GetComponent<Camera>().aspect * 5f;
        
        if(PortalReached && TheDeathWall.transform.position.x + DeathwallWidth >= cameraTransform.position.x - halfScreenLength && !PortalAndDeathWallReached)
        {
            TheDeathWall.GetComponent<WallMove>().enabled = false;
            PortalAndDeathWallReached = true;
            return;
        }
        else if (PortalReached) { return; }
        else if(Portal.transform.position.x <= cameraTransform.position.x + halfScreenLength)// && !PortalReached)
        {
            cameraTransform.position = new Vector3(Portal.transform.position.x - halfScreenLength, cameraTransform.position.y, cameraTransform.position.z);
            PortalReached = true;
            return;
        }

        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y + verticalOffset;

        Vector3 tmp = new Vector3(cameraTransform.position.x + Time.deltaTime * CameraSpeedForward, cameraTransform.position.y, cameraTransform.position.z);
        Vector3 camXY = new Vector3(cameraTransform.position.x, cameraTransform.position.y, 0);
        Vector3 playerXY = new Vector3(playerX, playerY, 0);
        Vector3 deltaPos = (playerXY - camXY);

 
        if (playerX > cameraTransform.position.x)// && !PortalReached) //if playerposx is more to the right than the camposx,then move
        {
            cameraTransform.position += new Vector3(deltaPos.x * CameraSpeedForward * Time.deltaTime, 0, 0);
        }
        else if(deltaPos.x < -HorizontalConstraint)// && !PortalReached)//if player is moving right
        {
            cameraTransform.position += new Vector3((deltaPos.x + HorizontalConstraint) * CameraSpeedForward * Time.deltaTime, 0, 0);

        }
        //Debug.Log("deltaY: " + deltaPos.y);
        if (deltaPos.y > VerticalUpConstraint)//if player is moving up more than constraint
        {
            cameraTransform.position += new Vector3(0, (deltaPos.y - VerticalUpConstraint) * CameraSpeedForward * Time.deltaTime, 0);
        }
        else if (deltaPos.y < -VerticalDownConstraint)
        {
            cameraTransform.position += new Vector3(0, (deltaPos.y + VerticalDownConstraint) * CameraSpeedForward * Time.deltaTime, 0);
        }//if player is moving up more than constraint
        


        //PUSH BY DEATHWALL\\
        //Jag, David, flyttade denna sats från första position till sista då det orsakade några problem. Jag lade också till en variabel för väggens bredd,
        //så att hela spriten syns, istället för halva. Justera breddvärdet när vi byter sprite för väggen.
        //UPPDATERING\\
        //Jag, återigen David, ändrade mig, variablen för bredden gjordes privat och hanteras i koden istället,
        //dessutom används den nu för att gömma väggen strax utanför kamerans synfält, eftersom de nya armarna är tydliga nog.
        if (Mathf.Abs(cameraTransform.position.x - (TheDeathWall.transform.position.x + DeathwallWidth)) < halfScreenLength)
        {
            cameraTransform.position = new Vector3((TheDeathWall.transform.position.x + DeathwallWidth) + halfScreenLength, cameraTransform.position.y, cameraTransform.position.z);
        }
        
    }

   
}
