using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator anim;
	private CharacterController controller;

	public Vector3 moveDirection = Vector3.zero;

	public float gravity = 25.0f;
	public float speed = 7.0f;
	public float jumppower = 12.0f;
	public float playerHealthPoint = 10.0f;

	public GameObject shield;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		shield.SetActive(false);
	}
    void Update()
	{
		playerMovement();
		shieldManage();
		if (speed > 0)
        {
			anim.SetFloat("Speed", controller.velocity.magnitude);
		}
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

	void shieldManage()
    {
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (shield.activeSelf == true)
			{
				shield.SetActive(false);
				shield.GetComponent<Shield>().guardTime = 0;
			}
			else if (shield.activeSelf == false)
			{
				shield.SetActive(true);
			}
		}
	}

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DeathArea" || other.tag == "obstacle")
        {
			//SceneManager.LoadScene("Stage 2");
			Debug.Log("dead");
        }

		if(other.tag == "JumpPad")
        {
			moveDirection.y = 15.0f;
        }

		if(other.tag == "DoorOpenSwitch")
		{
			GameObject.FindGameObjectWithTag("Door").SetActive(false);
		}

		if(other.tag == "DoorCloseSwitch")
        {
			GameObject.Find("Stage").transform.Find("BossDoor").gameObject.SetActive(true);
		}

		if(other.tag == "Bullet")
        {
			playerHealthPoint -= 1.0f;	
        }

		if(other.tag == "moveLR" || other.tag == "moveUD")
        {
			gameObject.transform.SetParent(other.gameObject.transform);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "moveLR"|| other.tag == "moveUD")
		{
			gameObject.transform.SetParent(null);
		}
	}
}

