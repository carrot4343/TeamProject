using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class z : MonoBehaviour
{
    Vector3 pos; //������ġ
    public float delta = 2.0f; // ��(��)�� �̵������� (x)�ִ밪
    public float speed = 3.0f; // �̵��ӵ�

    void Start()
    {
        pos = transform.position;
    }


    void Update()
    {
        Vector3 v = pos;
        v.z += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
