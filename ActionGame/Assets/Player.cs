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
		//�ִϸ��̼� ��Ʈ��
		if (speed > 0)
			anim.SetFloat("Speed", controller.velocity.magnitude);

		//��Ʈ�ѷ��� �ٴڿ� ��������� �������� ������ �� ����
		if (controller.isGrounded)
		{

			moveDirection = transform.forward * Input.GetAxis("Horizontal") * speed;
			moveDirection.y = -gravity * Time.deltaTime;
			//���� ����
			if (Input.GetButtonDown("Jump"))
			{
				moveDirection.y = jumppower;
			}
		}
		else
		{
			//�߷��� ���� ������. gravity ������ �����ϸ� �߷��� �� ũ�� �ϰų� �۰� �� �� ����.
			moveDirection.y -= gravity * Time.deltaTime;
		}
		//������ ����
		controller.Move(moveDirection * Time.deltaTime);
	}
}
