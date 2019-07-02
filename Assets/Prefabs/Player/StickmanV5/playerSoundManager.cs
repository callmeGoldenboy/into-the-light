using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundManager : MonoBehaviour
{
    public AudioClip FootStepL;
    public AudioClip FootStepR;
    public AudioClip Jump;
    public AudioClip Landing;
    public AudioClip deathByWall;
    public AudioClip deathByTrap;

    public bool FootStepLb;
    public bool FootStepRb;
    public bool Jumpb;
    public bool Landingb;
    public bool deathByWallb;
    public bool deathByTrapb;

    public bool generalStep;
    private bool generalHelp;
    private bool dead;

    // Start is called before the first frame update
    void Start()
    {
       // gameObject.GetComponent<AudioSource>().clip = FootStepL;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead) { return; }
        if (FootStepLb) { playAudioClip(FootStepL); FootStepLb = false; }
        else if (FootStepRb) { playAudioClip(FootStepR); FootStepRb = false; }
        else if (Jumpb) { playAudioClip(Jump); Jumpb = false; }
        else if (Landingb) { playAudioClip(Landing); Landingb = false; }
        else if (generalStep)
        {
            generalStep = false;
            if (generalHelp) { FootStepLb = true; generalHelp = false; }
            else { FootStepRb = true; generalHelp = true; }
        }
        else if (deathByWallb) { playAudioClip(deathByWall); deathByWallb = false; dead = true; }
        else if (deathByTrapb) { playAudioClip(deathByTrap); deathByTrapb = false; dead = true; }
    }
    private void playAudioClip(AudioClip AC)
    {
        gameObject.GetComponent<AudioSource>().enabled = false;
        gameObject.GetComponent<AudioSource>().clip = AC;
        gameObject.GetComponent<AudioSource>().enabled = true;
    }
}
