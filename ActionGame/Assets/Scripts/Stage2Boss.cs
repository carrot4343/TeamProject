using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Boss : MonoBehaviour
{
    public int bossHealthPoint;
    void Start()
    {
        bossHealthPoint = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealthPoint == 0)
        {
            //SceneManager.LoadScene("Stage 3");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            bossHealthPoint -= 1;
            Destroy(other.gameObject);
        }
    }
}
