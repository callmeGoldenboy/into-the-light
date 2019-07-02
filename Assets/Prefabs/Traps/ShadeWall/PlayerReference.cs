/* README
 * PLAYER REFERENCE - BESKRIVNING
 * ====================================
 * Detta skript är avsett att underlätta implementationen av dödsväggen i en scen. 
 * Ingen av dödsväggens spelar-referenser behöver anges manuellt. Ge istället
 * referensen till detta skript, så delas det med resten av skripten som behöver det.
 * (Detta skript måste uppdateras manuellt om fler skript behöver referensen)
 * 
 * PLAYER
 * ====================================
 * Referens till spelaren's avatar, det är denna som delas med resten av skripten. * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReference : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //WALLMOVE
        WallMove wallMove = this.GetComponent<WallMove>();                      //Retrieve the WallMove script.
        wallMove.SetPlayer(this.player);                                        //Share player reference with WallMove script.
        //OVALMOVE
        OvalMove[] ovalMoves = this.GetComponentsInChildren<OvalMove>();        //Retrieve all OvalMove Scripts.
        foreach (OvalMove o in ovalMoves)                                       //Share player reference with each OvalMove script.
        {
            o.SetPlayer(this.player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
