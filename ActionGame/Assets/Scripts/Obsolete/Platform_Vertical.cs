using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Vertical : MonoBehaviour
{
    Vector3 pos; //������ġ
    public float delta = 20.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 1.0f; // �̵��ӵ�

    void Start()
    {
        pos = transform.position;
    }


    void Update()
    {
        Vector3 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
