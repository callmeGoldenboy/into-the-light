using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*README
Description: 
 Every restart of levels has to be done through this script

Public variables:
None


Dependencies: 
Other scripts must activate the restarting, this script will not restart level by itself

How to use:
Drag to any suitable object  */

public class restartLevel : MonoBehaviour
{
    private Scene sceneToRestart;
    // Start is called before the first frame update
    void Update()
    {
     
    }
    void Start()
    {
        sceneToRestart = SceneManager.GetActiveScene(); //This current scene is the one that should be reloaded
    }

    
    public void changeScene(Scene newScene)
    {
        sceneToRestart = newScene; //For level designs 
    }

    public void restartScene()
    {   
        
        Application.LoadLevel(sceneToRestart.name);
    }
}
