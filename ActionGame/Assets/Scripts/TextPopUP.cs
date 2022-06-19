using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUP : MonoBehaviour
{

    public bool isVisible;
    GameObject myText;

    
    // Start is called before the first frame update

    void awake()
    {
        isVisible = false;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible)
        {
            myText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isVisible = true;
    }
}
