using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	public float speed = 6.0f;
	public float turnSpeed = 90.0f;

	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;

	public float jumppower = 7.0f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	void Update()
	{
		//애니메이션 컨트롤
		if (speed > 0)
			anim.SetFloat("Speed", controller.velocity.magnitude);

		//컨트롤러가 바닥에 닿아있을때 움직임을 제어할 수 있음
		if (controller.isGrounded)
		{

			moveDirection = transform.forward * Input.GetAxis("Horizontal") * speed;
			moveDirection.y = -gravity * Time.deltaTime;
			//점프 제어
			if (Input.GetButtonDown("Jump"))
			{
				moveDirection.y = jumppower;
			}
		}
		else
		{
			//중력을 직접 설정함. gravity 변수를 조정하면 중력을 더 크게 하거나 작게 할 수 있음.
			moveDirection.y -= gravity * Time.deltaTime;
		}
		//움직임 제어
		controller.Move(moveDirection * Time.deltaTime);
	}
}
