using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstablePlattformTrigger : MonoBehaviour
{
    
    public float time = 2f;
    private BoxCollider2D [] bcollider;
    public Trap TrapToActivate;


    // Start is called before the first frame update
    void Start()
    {
       bcollider = gameObject.GetComponents<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            StartCoroutine(WaitingTime(time));
           
            

        }
    }
    IEnumerator WaitingTime(float t)
    {
       
        yield return new WaitForSeconds(t);
        TrapToActivate.activate();
    }
}
