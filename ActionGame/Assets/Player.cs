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
		//Animation Control
		if (speed > 0)
			anim.SetFloat("Speed", controller.velocity.magnitude);

		//Can control when controller is on ground
		if (controller.isGrounded)
		{

			moveDirection = transform.forward * Input.GetAxis("Horizontal") * speed;
			moveDirection.y = -gravity * Time.deltaTime;
			//Control jump
			if (Input.GetButtonDown("Jump"))
			{
				moveDirection.y = jumppower;
			}
		}
		else
		{
			//Set gravity
			moveDirection.y -= gravity * Time.deltaTime;
		}
		//Control Movement
		controller.Move(moveDirection * Time.deltaTime);
	}
}
