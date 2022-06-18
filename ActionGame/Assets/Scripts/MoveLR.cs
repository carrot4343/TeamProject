using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLR : MonoBehaviour
{
	private Vector3 leftMaxMovePosition, rightMaxMovePosition;
	private float rangeMotion, moveDirection;
	public float speed;
	void Start()
	{
		rangeMotion = 5.0f;
		speed = 1.0f;
		moveDirection = 1;
		leftMaxMovePosition = gameObject.transform.position - new Vector3(0, 0, rangeMotion);
		rightMaxMovePosition = gameObject.transform.position + new Vector3(0, 0, rangeMotion);
	}
	void Update()
	{
		if (gameObject.transform.position.x > rightMaxMovePosition.x || gameObject.transform.position.z < leftMaxMovePosition.z)
        {
			moveDirection = -moveDirection;
        }
		gameObject.transform.Translate(new Vector3( moveDirection * speed * Time.deltaTime, 0, 0));
	}

    

    }
