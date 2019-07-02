using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneFader : MonoBehaviour
{

    public Image img;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }


    public void FadeTo (int sceneindex)
    {
        StartCoroutine(FadeOut(sceneindex));
    }
   
    IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0; 
              
        }
    }

    //a black canvas will be displayed and then faded out before loading next scene

    IEnumerator FadeOut (int Sceneindex)
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime;
            img.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        SceneManager.LoadScene(Sceneindex);
    }
   

}
