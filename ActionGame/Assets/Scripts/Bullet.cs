using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 1;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 350));
        Destroy(gameObject, 4.0f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("damaged");
            Destroy(this.gameObject);
            GameObject.Find("Player").GetComponent<HealthManager>().HurtPlayer(bulletDamage, Vector3.zero);
        }
    }
}
