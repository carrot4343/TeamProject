using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 350));
        Destroy(gameObject, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}