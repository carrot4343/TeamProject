using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public float interval;

    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
