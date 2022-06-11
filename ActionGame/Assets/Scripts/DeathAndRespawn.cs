using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private List<GameObject> deathZones;
    [SerializeField] private Vector3 vectorPoint;
    [SerializeField] private float dead;
    

    private void Update()
    {
        
      
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            vectorPoint = player.transform.position;
            Destroy(other.gameObject);
        }

        else if (other.gameObject.tag == "Deathzone")
        {
            player.transform.position = vectorPoint;
        }

    }

}
