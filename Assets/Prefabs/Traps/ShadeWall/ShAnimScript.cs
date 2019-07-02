/* README
 * SHADE ANIMATION SCRIPT - BESKRIVNING
 * ====================================
 * Ett litet skript som initierar vissa animationsparametrar med slumpade värden. Det är allt.
 * Om ni vill justera parametrarna får ni gör det här i koden.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShAnimScript : MonoBehaviour
{
    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = this.GetComponent<Animator>();
        anime.SetFloat("SpeedModifier", Random.Range(0.1f, 1.5f));
        anime.SetFloat("AnimOffset", Random.Range(0f, 10f));
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
