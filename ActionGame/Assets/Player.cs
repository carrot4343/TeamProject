using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	public Vector3 moveDirection = Vector3.zero;

	public float gravity = 25.0f;
	public float speed = 7.0f;
	public float jumppower = 12.0f;


	[SerializeField] private Transform player;
	[SerializeField] private List<GameObject> checkPoints;
	[SerializeField] private List<GameObject> deathZones;
	[SerializeField] private Vector3 vectorPoint;

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
			anim.SetBool("isJump", false);
			moveDirection = transform.forward * Input.GetAxis("Horizontal") * speed;
			moveDirection.y = -gravity * Time.deltaTime;
			//Control jump
			if (Input.GetButtonDown("Jump"))
			{
				moveDirection.y = jumppower;
				anim.SetBool("isJump", true);
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

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "CheckPoint")
		{
			vectorPoint = player.transform.position;
			Destroy(other.gameObject);
		}

		else if (other.gameObject.tag == "Deathzone")
		{
			player.transform.position = vectorPoint;
		}

	}

}
