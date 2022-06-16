using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()//Rotate Character when press A, D
    {
        if (Input.GetKeyDown(KeyCode.A) && transform.rotation.y == 0)
        {
            transform.Rotate(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.rotation.y != 0)
        {
            transform.Rotate(0, -180, 0);
        }
    }
}
