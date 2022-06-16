using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockSpawner : MonoBehaviour
{
    public GameObject obstacle;
    public float interval;

    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(obstacle, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
