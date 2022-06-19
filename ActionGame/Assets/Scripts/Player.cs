using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private List<GameObject> checkPoints;
	[SerializeField] private List<GameObject> deathZones;
	[SerializeField] private Vector3 vectorPoint;
	[SerializeField] private float dead;

	private Animator anim;
	private CharacterController controller;
	private GameObject hpSlider;
	public HealthManager healthmanager;

	public Vector3 moveDirection = Vector3.zero;

	public float gravity = 25.0f;
	public float speed = 7.0f;
	public float jumppower = 12.0f;
	
	public float knockBackForce;
	public float knockBackTime;
	private float knockBackCounter;

	public GameObject shield;

    void Start()
	{
		player = this.transform;
		controller = GetComponent<CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
		healthmanager = FindObjectOfType<HealthManager>();
		shield.SetActive(false);
		vectorPoint = gameObject.transform.position;
		hpSlider = GameObject.Find("Canvas/PlayerHP");
	}

	void Update()
	{
		hpSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.8f, 0));
		hpSlider.GetComponent<Slider>().value = healthmanager.currentHealth;

		if (knockBackCounter <=0)
        {
			shieldManage();
			playerMovement();
		}
        else
        {
			knockBackCounter -= Time.deltaTime;
        }


        if (speed > 0)
		{
			anim.SetFloat("Speed", controller.velocity.magnitude);
		}

		if(healthmanager.currentHealth <= 0)
        {
			player.transform.position = vectorPoint;
			healthmanager.currentHealth = 10;
			GameObject.Find("Boss").GetComponent<Stage2Boss>().bossHealthPoint = 10;
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

	public void KnockBack(Vector3 direction)
    {
		knockBackCounter = knockBackTime;

		moveDirection = direction * knockBackForce;
		//moveDirection.y = knockBackForce;

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
		if (other.gameObject.tag == "CheckPoint")
		{
			vectorPoint = player.transform.position;
			Destroy(other.gameObject);
		}

		else if (other.gameObject.tag == "Deathzone")
		{
			player.transform.position = vectorPoint;
		}

		if (other.tag == "JumpPad")
		{
			moveDirection.y = 15.0f;
		}

		if (other.tag == "DoorOpenSwitch")
		{
			GameObject.FindGameObjectWithTag("Door").SetActive(false);
		}

		if (other.tag == "DoorCloseSwitch")
		{
			GameObject.Find("Stage").transform.Find("BossDoor").gameObject.SetActive(true);
		}

		if (other.tag == "Platform")
		{
			gameObject.transform.SetParent(other.gameObject.transform);
		}

		if (other.tag == "Pusher")
		{
			moveDirection.x = 10.0f;
		}
    }
    void OnTriggerExit(Collider other)
	{
		if (other.tag == "Platform")
		{
			gameObject.transform.SetParent(null);
		}
		
	}
}

