using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    public int rollingRockDamage = 4;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 20.0f, -300.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            FindObjectOfType<HealthManager>().HurtPlayer(rollingRockDamage, Vector3.zero);
        }
    }
}
