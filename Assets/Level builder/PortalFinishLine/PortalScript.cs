using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PortalScript : MonoBehaviour
{
    public float PortalSnapSeconds = 2f;

    public float OffsetColorRed = 0;
    public float OffsetColorGreen = 0;
    public float OffsetColorBlue = 0;
    public float OffsetRange = 0;
    private bool proceed = false;

    private bool levelFinished = false;
    private Light portalLight;
    private PulsingLight pulsingScript;
    private float defaultIntensity; //I will extract pulsingLight script in light to disable it

    
    // Start is called before the first frame update
    void Start()
    {
        portalLight = gameObject.GetComponentInChildren<Light>();
        pulsingScript = gameObject.GetComponentInChildren<PulsingLight>();
        defaultIntensity = pulsingScript.GetDefaultIntensity();



    }

    // Update is called once per frame
    void Update()
    {   if(defaultIntensity <= 0)
        {
            defaultIntensity = portalLight.intensity; //Ibland blir defaultintensity = 0, speciellt när scenen laddas om
        }
        if (levelFinished && portalLight.intensity > 0) { afterLevelFinished(); }
        else if (levelFinished && proceed== false)
        {
            proceedToNextLevel();
            proceed = true;
        }
    }

     void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if(colliderObject.tag == "Player")
        {
            GameObject.Destroy(colliderObject.gameObject); //Destroy player
            gameObject.AddComponent<AudioListener>(); //Add audio listener on portal since player is destroyed
            levelFinished = true;
            LevelFinished();
            
            
        }
    }

    private void LevelFinished()
    {
        //This will be done once(!) when level finished
        
        pulsingScript.enabled = false; //Turn off pulsing light so that we can turn it off

    }

    private void afterLevelFinished()
    {
        
        portalLight.intensity -= (defaultIntensity / PortalSnapSeconds) * Time.deltaTime; //Fade out the light
        portalLight.color = portalLight.color + new Color(OffsetColorRed, OffsetColorGreen,OffsetColorBlue);
        portalLight.range += OffsetRange;

        gameObject.GetComponentInChildren<AudioSource>().pitch -= 0.01f;
        

    }
    private void proceedToNextLevel()
    {
        gameObject.GetComponentInChildren<AudioSource>().enabled = false;
        gameObject.GetComponentsInChildren<AudioSource>()[1].enabled = true;

        //gameObject.GetComponent<AudioListener>().enabled = false; //Stop listening
        
        if(PlayerPrefs.GetInt("currentLevel") == SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel") + 1);//Increment only when end met
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Always play the next
    }
}
