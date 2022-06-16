using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Resume : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject resumeButton, mainMenuButton, pausedText;
    void Start()
    {
        resumeButton.SetActive(false);
        mainMenuButton.SetActive(false);
        pausedText.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PausedButtonPressed()
    {
        Time.timeScale = 0;
        gameObject.SetActive(false);
        resumeButton.SetActive(true);
        mainMenuButton.SetActive(true);
        pausedText.SetActive(true);
    }
}
