using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool isPause = false;
    public GameObject conButton;
    public void PauseGame()
    {
        if (isPause)
        {
            isPause = false;
        }
        else
        {
            conButton.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPause = true;
            
        }
    }
}
