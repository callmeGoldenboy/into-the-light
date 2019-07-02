using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{   /*README
    This script can be added to any light and the light will pulsate by changing intensity over time
    IMPORTANT!! This script assumes your default intensity is max
    
    Use MinimumPercentageOfIntensity to define how low intensity is acceptable relative to default
    LoopSeconds define how many seconds to return to normal intensity again
     */
    public bool Enabled = true;
    public float MinimumPercentageOfIntensity = 0.8f;
    public float LoopSeconds = 3;

    //private Light gameObject.GetComponent<Light>();
    private float defaultIntensity;
    private float targetIntensityChange;
    private float deltaIntensityPerFrame;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Light>() = gameObject.GetComponent<Light>();
        defaultIntensity = gameObject.GetComponent<Light>().intensity;
        targetIntensityChange = defaultIntensity * (1 - MinimumPercentageOfIntensity);
        deltaIntensityPerFrame = targetIntensityChange / (LoopSeconds / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) { return;}
        //Debug.Log(gameObject.GetComponent<Light>().intensity.ToString());
        gameObject.GetComponent<Light>().intensity += deltaIntensityPerFrame * Time.deltaTime;
        if (gameObject.GetComponent<Light>().intensity < defaultIntensity - targetIntensityChange && deltaIntensityPerFrame < 0)
        {
            deltaIntensityPerFrame *= -1;
        }
        else if(gameObject.GetComponent<Light>().intensity > defaultIntensity && deltaIntensityPerFrame > 0)
        {
            deltaIntensityPerFrame *= -1;
        }
        //Debug.Log(gameObject.GetComponent<Light>().intensity.ToString());
    }
    public float GetDefaultIntensity() { return defaultIntensity; }
}
