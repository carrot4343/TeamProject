using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2Boss : MonoBehaviour
{
    public int bossHealthPoint;
    private GameObject hpSlider;
    void Start()
    {
        hpSlider = GameObject.Find("Canvas/BossHP");
        bossHealthPoint = 20;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
        if(bossHealthPoint == 0)
        {
            SceneManager.LoadScene("win");
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
