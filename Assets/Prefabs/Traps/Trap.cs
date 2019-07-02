using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*README
 This is the abstract super class for all traps, the idea is that when we use triggers the actual trigger 
 does not care what kind of trap it is, it will just call the function activate()*/

public abstract class Trap : MonoBehaviour
{
    public abstract void activate();
}
