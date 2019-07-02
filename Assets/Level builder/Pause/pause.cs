using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle();
        }
    }
    public void PauseToggle()
    {
        Debug.Log("Pause");
        canvas.SetActive(!canvas.activeSelf);                               //Activate/Deactivate the entire canvas.
        //foreach (Button b in GetComponentsInChildren<Button>()) { b.enabled = !b.enabled; }
        Time.timeScale = (Time.timeScale + 1)%2;
        
    }

    public void QuitToMenu()
    {
        PauseToggle();                                      //Fixes black screen upon returning to menu. Don't ask why.
        SceneManager.LoadScene(0);        
    }

    public void restartThisLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

}
