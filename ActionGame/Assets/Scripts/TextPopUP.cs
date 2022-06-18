using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUP : MonoBehaviour
{

    public GameObject myText;
    public bool isVisible;

    
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
            myText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isVisible = true;

    }
}
