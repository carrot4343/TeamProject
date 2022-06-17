using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLR : MonoBehaviour
{
	public Vector3 leftMaxMovePosiion, rightMaxMovePosition;
	private float rangeMotion, moveDirection;
	public float speed;
	void Start()
	{
		rangeMotion = 6.0f;
		speed = 3.5f;
		moveDirection = 1;
		leftMaxMovePosiion = gameObject.transform.position - new Vector3(0, 0, rangeMotion);
		rightMaxMovePosition = gameObject.transform.position + new Vector3(0, 0, rangeMotion);
	}
	void Update()
	{
		if (gameObject.transform.position.z > rightMaxMovePosition.z || gameObject.transform.position.z < leftMaxMovePosiion.z)
        {
			moveDirection = -moveDirection;
        }
		gameObject.transform.Translate(new Vector3(0, 0, moveDirection * speed * Time.deltaTime));
	}
}
