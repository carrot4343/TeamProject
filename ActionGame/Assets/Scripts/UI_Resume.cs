using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Resume : MonoBehaviour
{
    public GameObject pauseButton, mainMenuButton, pausedText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeButtonPressed()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        pauseButton.SetActive(true);
        mainMenuButton.SetActive(false);
        pausedText.SetActive(false);
    }
}
