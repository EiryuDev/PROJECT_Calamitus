using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRLD_ANIMATION_TRIGGER_SCENE : MonoBehaviour
{
    public GameObject creditsPanel;


    public void ReloadScene()
    {
        creditsPanel.SetActive(false);
    }
}
