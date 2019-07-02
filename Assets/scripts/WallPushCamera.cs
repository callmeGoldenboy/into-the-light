/* README
 * UTDATERAD KOD, ANVÄND CAMERA SCRIPT ISTÄLLET!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPushCamera : MonoBehaviour
{
	public GameObject wall;
	public float wallWidth;
	public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float minDist = wall.transform.position.x - wallWidth/2f + this.GetComponent<Camera>().aspect * 5f;
        if(player.GetComponent<Transform>().position.x < minDist)
		{
			this.GetComponent<Transform>().position = new Vector3(minDist, 0f, -10f);
		}
		else this.GetComponent<Transform>().position = new Vector3(player.GetComponent<Transform>().position.x, 0f, -10f);
    }
}
