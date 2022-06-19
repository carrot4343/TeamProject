using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float spawnDelay = 5f;
    public int healToGive = 10;
   // private bool startCnt;
    void Start()
    {
        //startCnt = false;
        gameObject.SetActive(true);
    }



    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<HealthManager>().HealPlayer(healToGive);
            gameObject.SetActive(false);
            //startCnt = true;
        }
    }
}
