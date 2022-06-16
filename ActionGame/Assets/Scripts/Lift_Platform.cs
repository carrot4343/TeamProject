using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_Platform : MonoBehaviour
{

    // ���� ��ó : https://kupaprogramming.tistory.com/29

    public bool debugLine = true; //�̵���� ǥ��
    public Vector3[] relativeMovePoint;  // �����ǥ
    public bool awakeStart = true;  // ���� �� ����, false�� �����ϸ� �ö�Ż �� ����
    public float awakeWaitTime = 0f;  // ���� �� ������ ��� �ð�
    public float speed = 3f;  // �̵� �ӵ�
    public float waitTime = 1.0f; // ������ ��� �ð�
    public float destroyWaitTime = 1.0f; // ���� �� ���� ��� �ð�
    public float respawnTime = 1.0f; // ����� �ð�
    public bool jumpInertia = false; // ���� ���� ����

    private float jumpInertiaValue; // jumpInertia�� true�� �� ������ ����

    private int cur = 1; // ���� ������ ��� ��ȣ
    private bool back = false; // ���ư��� ���ΰ�
    private bool movingOn = false; // �����̴� ���ΰ�, awakeStart false�� �ʿ�

    private bool playerCheck = false;  // ž���ߴ°�
    private GameObject player;
    private CharacterController playerCC;
    private Player playerScript;

    Vector3[] Pos;  // �����ǥ���� ���� ��ȯ�� ���� ���� ��ǥ�� ������ �ִ� �迭
    Vector3 firstPos = Vector3.zero;  // OnDrawGizoms���� ���, ���� ���� �ľ�, �ʱ� ��ǥ ����

    private void Awake()
    {
        if(relativeMovePoint.Length <= 0) // �̵���ΰ� �Էµ��� ������ ��ũ��Ʈ ������Ʈ ����
        {
            Destroy(this);
            return;
        }

        player = GameObject.Find("Player");  // �÷��̾� ã��
        if(player)
        {
            playerScript = player.GetComponent<Player>();
            playerCC = player.GetComponent<CharacterController>();
        }

        Pos = new Vector3[relativeMovePoint.Length + 1];  // ���� ��ǥ�� �����ϱ� ���� +1
        Pos[0] = firstPos = transform.position;  // ���� ��ǥ ����, firstPos�� OnDrawGizmos�� ���� ���

        for(int i = 1; i < relativeMovePoint.Length + 1; i++)
        {
            Pos[i] = Pos[i - 1] + transform.TransformDirection(relativeMovePoint[i - 1]); ;
        }
        if(awakeStart)
        {
            movingOn = true;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        WaitForFixedUpdate delay = new WaitForFixedUpdate();

        if(awakeStart && awakeWaitTime != 0)
        {
            yield return new WaitForSeconds(awakeWaitTime);
        }

        while(true)
        {
            if(transform.position == Pos[cur])
            {
                if(!back)
                {
                    if(++cur == Pos.Length)
                    {
                        if(!awakeStart)
                        {
                            Invoke("DestroyWait", destroyWaitTime);
                            this.enabled = false;
                            yield break;
                        }
                        else
                        {
                            back = true;
                            cur = cur - 2;
                        }
                    }
                }
                else
                {
                    if (--cur == -1)
                    {
                        back = false;
                        cur = cur + 2;
                    }
                }
                if(waitTime != 0)
                {
                    yield return new WaitForSeconds(waitTime);
                }
                else
                {
                    yield return delay;
                }
            }
            else
            {
                Vector3 prevPos = transform.position;

                transform.position = Vector3.MoveTowards(transform.position, Pos[cur], speed * Time.deltaTime);
                if(playerCheck)
                {
                    playerCC.Move(transform.position - prevPos);
                    playerCC.Move(new Vector3(0, -2.0f * Time.deltaTime, 0));

                    if (jumpInertia)
                    {
                        jumpInertiaValue = ((Pos[cur] - transform.position).normalized * speed).y;
                    }
                }
                yield return delay;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(!debugLine || relativeMovePoint.Length <= 0)
        {
            return;
        }
        Vector3 t1, t2;
        if (firstPos == Vector3.zero)
            t1 = t2 = transform.position;
        else
            t1 = t2 = firstPos;

        for(int i = 0; i < relativeMovePoint.Length; i++)
        {
            t2 += transform.TransformDirection(relativeMovePoint[i]);
            if(0 < i)
            {
                t1 += transform.TransformDirection(relativeMovePoint[i - 1]);
            }
            Debug.DrawLine(t1, t2, Color.red);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            Debug.Log("Hello!");
            playerCheck = true;
            if(!movingOn)
            {
                movingOn = true;
                StartCoroutine("Move");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCheck = false;

            /*
            if (jumpInertia)
            {
                playerScript.NowMoveY += jumpInertiaValue;
            }
            */
        }
    }

    private void DestroyWait()
    {
        this.gameObject.SetActive(false);
        Invoke("Respawn", respawnTime);
    }

    private void Respawn()
    {
        cur = 1;
        transform.position = firstPos;
        back = false;
        movingOn = false;
        playerCheck = false;
        this.enabled = true;
        this.gameObject.SetActive(true);
    }

}
