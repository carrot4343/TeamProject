using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_Platform : MonoBehaviour
{

    // 참고 출처 : https://kupaprogramming.tistory.com/29

    public bool debugLine = true; //이동경로 표시
    public Vector3[] relativeMovePoint;  // 상대좌표
    public bool awakeStart = true;  // 시작 시 동작, false로 설정하면 올라탈 때 동작
    public float awakeWaitTime = 0f;  // 시작 후 움직임 대기 시간
    public float speed = 3f;  // 이동 속도
    public float waitTime = 1.0f; // 경유지 대기 시간
    public float destroyWaitTime = 1.0f; // 도착 시 삭제 대기 시간
    public float respawnTime = 1.0f; // 재생성 시간
    public bool jumpInertia = false; // 점프 관성 포함

    private float jumpInertiaValue; // jumpInertia가 true일 때 점프력 가감

    private int cur = 1; // 현재 가야할 경로 번호
    private bool back = false; // 돌아가는 중인가
    private bool movingOn = false; // 움직이는 중인가, awakeStart false시 필요

    private bool playerCheck = false;  // 탑승했는가
    private GameObject player;
    private CharacterController playerCC;
    private Player playerScript;

    Vector3[] Pos;  // 상대좌표값을 토대로 변환한 실제 월드 좌표를 가지고 있는 배열
    Vector3 firstPos = Vector3.zero;  // OnDrawGizoms에서 사용, 시작 상태 파악, 초기 좌표 저장

    private void Awake()
    {
        if(relativeMovePoint.Length <= 0) // 이동경로가 입력되지 않으면 스크립트 컴포넌트 삭제
        {
            Destroy(this);
            return;
        }

        player = GameObject.Find("Player");  // 플레이어 찾기
        if(player)
        {
            playerScript = player.GetComponent<Player>();
            playerCC = player.GetComponent<CharacterController>();
        }

        Pos = new Vector3[relativeMovePoint.Length + 1];  // 최초 좌표를 저장하기 위해 +1
        Pos[0] = firstPos = transform.position;  // 최초 좌표 저장, firstPos는 OnDrawGizmos를 위해 사용

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
