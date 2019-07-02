using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmostGrounded : MonoBehaviour
{
    public GameObject Player;
    private int grounds = 0;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision detected");
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<PlayerMovement>().isGrounded = true;
            grounds++;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("collision exit");
        if (collision.collider.tag == "Ground")
        {
            if (--grounds == 0)
                Player.GetComponent<PlayerMovement>().isGrounded = false;
            else if (grounds < 0)
                Debug.LogError("Grounds less than 0, something went wrong, check it out, NOW!");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Ground")
        {
            Player.GetComponent<PlayerMovement>().AlmostGrounded = true;
            grounds++;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.tag == "Ground")
        {
            if (--grounds == 0)
                Player.GetComponent<PlayerMovement>().AlmostGrounded = false;
            else if (grounds < 0)
                Debug.LogError("Grounds less than 0, something went wrong, check it out, NOW!");
        }
    }
}
