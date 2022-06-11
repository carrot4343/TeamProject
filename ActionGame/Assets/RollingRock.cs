using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 20.0f, -300.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
