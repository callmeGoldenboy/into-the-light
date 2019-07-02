using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public SoundHelper[] sounds;
    private string music = "musicEnabled";
    public bool castleMusic = true;

    // Start is called before the first frame update
    void Awake()
    {
        


        //setting the audio managers parameters equals to my parameters 
        foreach (SoundHelper s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
         
        }
        
    }
    void Start()
    {



        ActivateMusic();
        if (gameObject.activeSelf)
        {
            StartCoroutine(PlayMusic(castleMusic));
        }
       
       
       
      


    }

    public void Play(string name)
    {
        SoundHelper s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound isnt there");
            return;
        }
        StartCoroutine(FadeIn(s.source, 6f));

    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.PlayDelayed(1f);
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
    
    //function that returns the length of the sound
    private float Soundlength(string name)
    {
        SoundHelper s = Array.Find(sounds, sound => sound.name == name);
        float length = s.clip.length;
        return length;
    }

    //used to play the two diffrent background sounds one after another without delay
    IEnumerator PlayMusic(bool music)
    {
        if (music) {
            float waitTime = Soundlength("MusicStart") - 2f;
            Play("MusicStart");
            yield return new WaitForSeconds(waitTime);
            Play("MusicLoop");
        }
        else
        {
            float waitTime = Soundlength("MusicStartForest") -4f;
            Play("MusicStartForest");
            yield return new WaitForSeconds(waitTime);
            Play("MusicLoopForest");
        }
        
;
    }

    public void ActivateMusic()
    {
        //Debug.Log(PlayerPrefs.GetInt(music));

        if (PlayerPrefs.GetInt(music) == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

 
   

}
