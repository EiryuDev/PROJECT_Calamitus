using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("BUTTON DATA")]
    public GameObject quitButton;

    private void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            // Disable the target object if running in WebGL
            if (quitButton != null)
            {
                quitButton.SetActive(false);
            }
        }
        else
        {
            // Enable the target object for other platforms
            if (quitButton != null)
            {
                quitButton.SetActive(true);
            }
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
   

}
