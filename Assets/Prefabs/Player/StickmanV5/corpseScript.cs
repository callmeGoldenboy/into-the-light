using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corpseScript : MonoBehaviour
{
    public float collapseForce = 2f;
    public GameObject ragdoll;
    private SpriteRenderer[] spriteComponents;
    private GameObject[] spriteHolders;
    public bool collapse = false;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (collapse) { collapseActivate(); }
    }

    public void collapseActivate()
    {
        collapse = false;
        SpriteRenderer[] ragdollSprites = ragdoll.GetComponentsInChildren<SpriteRenderer>();
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;

        //ragdoll.AddComponent<AudioListener>();
        GameObject tmp = GameObject.Instantiate(ragdoll, new Vector3(transform.position.x,transform.position.y+1.3f,0), new Quaternion());
        //Destroy(gameObject);
        foreach(Rigidbody2D RB2 in tmp.GetComponentsInChildren<Rigidbody2D>()) { RB2.AddForce(currentVelocity); }
        Component.Destroy(gameObject.GetComponent<Rigidbody2D>());
        //Destroy(gameObject);
    }

}
