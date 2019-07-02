using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public bool reset = false;
    public Button[] levels;
    private string current = "currentLevel";
    private string music = "musicEnabled";
    public Sprite musicOn;
    public Sprite musicOff;
    public Button musicButton;
    public AudioSource EnableBackgroundMusic;
    public SceneFader fader;
    void Start()
    {

       
        if (!PlayerPrefs.HasKey(music))
        {
            PlayerPrefs.SetInt(music, 1);
        }
        if(PlayerPrefs.GetInt(music) == 1)
        {
            musicButton.image.overrideSprite = musicOn;
            EnableBackgroundMusic.enabled = true;
        }
        else
        {
            musicButton.image.overrideSprite = musicOff;
            EnableBackgroundMusic.enabled = false;
        }
        

        if(!PlayerPrefs.HasKey(current)|| reset)
        {
            PlayerPrefs.SetInt(current, 1);
        }

        for(int i = 0; i < levels.Length; i++)
        {
            if(i < PlayerPrefs.GetInt(current))
            {
                levels[i].enabled = true;

            }
            else
            {
                levels[i].enabled = false;
            }
        }
        
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt(current) > levels.Length)
        {
            fader.FadeTo(levels.Length);//Replay last level when all unlocked
        }
        else
        {
            fader.FadeTo(PlayerPrefs.GetInt(current));
        }
    }

    public void SelectLevel(int levelIndex)
    {
        fader.FadeTo(levelIndex);

    }

    public void SetMusic ()
    {
        int newmusic = (PlayerPrefs.GetInt(music) + 1)%2;     
        PlayerPrefs.SetInt(music, newmusic);
        if(PlayerPrefs.GetInt(music) == 1)
        {
            musicButton.image.overrideSprite = musicOn;
            EnableBackgroundMusic.enabled = true;
        }
        else
        {
            musicButton.image.overrideSprite = musicOff;
            EnableBackgroundMusic.enabled = false;
        }  
        Debug.Log(PlayerPrefs.GetInt(music));
        


    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
