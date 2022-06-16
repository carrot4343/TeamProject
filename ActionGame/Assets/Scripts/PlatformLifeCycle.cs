using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLifeCycle : MonoBehaviour
{
    [SerializeField] float destroySec = 1f;
    private bool isHit;
    //CharacterController CC;
    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
        //CC = GetComponent<CharacterController>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, destroySec);
        }
    }
}
