using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float guardTime, parryingTime;
    void Start()
    {
        guardTime = 0;
        parryingTime = 0.2f;
    }

    
    void Update()
    {
        guardTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet" && guardTime <= parryingTime)
        {
            other.GetComponent<Transform>().rotation = Quaternion.Euler(90, 180, 0);
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-750));
        }
        else if(other.tag == "Bullet" && guardTime > parryingTime)
        {
            Destroy(other.gameObject);
        }
    }
}
