using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	private Vector3 moveDirection = Vector3.zero;

	public float gravity = 25.0f;
	public float speed = 7.0f;
	public float jumppower = 12.0f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}
	void Update()
	{
		playerMovement();

		if (speed > 0)
			anim.SetFloat("Speed", controller.velocity.magnitude);
	}
	void playerMovement()
	{
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

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathArea" || other.tag == "obstacle")
        {
			//SceneManager.LoadScene("Stage 2");
			Debug.Log("dead");
        }
    }
}

