using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HowToPlayExit : MonoBehaviour
{
    public GameObject howtoplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exitHowToPlay()
    {
        howtoplay.SetActive(false);
    }
}
