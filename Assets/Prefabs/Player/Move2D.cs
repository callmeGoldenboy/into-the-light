
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2D : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public bool isGrounded = false;

    private bool facingLeft = false;
    //private bool isJumping;       Bortkommenterad av David, används aldrig

    public bool GetFacingLeft() { return facingLeft; }
    public void SetFacingLeft(bool b) { this.facingLeft = b;}

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;


        animator.SetFloat("Speed", Mathf.Abs(movement.magnitude)*speed);
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        handleFlip(horizontal);
    }
    void handleFlip(float horizon)
    {
        /*  BORTKOMMENTERAD FÖR TILLFÄLLET AV DAVID
        if (horizon > 0 && !facingRight || horizon < 0 && facingRight)
        {
            //gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            facingRight = !facingRight;
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

        }*/
       
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            //Debug.Log("Jump!");
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            //isJumping = true;         Bortkommenterad av David, används aldrig
        }
    }



}
