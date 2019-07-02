
using UnityEngine;
/*Readme
 this is a helper class for AudioManager script. it has the parameters that we can change as desired taht will be applyied 
 to the audio sources*/
[System.Serializable]
public class SoundHelper 
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(0.1f,3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public bool isPlaying = false;

    [HideInInspector]
    public AudioSource source;
   
}
