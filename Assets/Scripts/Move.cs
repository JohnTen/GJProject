using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
	[SerializeField]
	private float speed = 5f;
	private bool canMove = true;

	[SerializeField]
	private float time_friend = 3.0f;
	[SerializeField]
	private float time_pull = 1f;
	[SerializeField]
	private float time_push = 0.5f;

	// Use this for initialization
	void Start()
	{

	}
	void MoveControlByTranslate()
	{

		if (Input.GetButtonDown("Jump"))
		{
			Invoke("Wait_pull", time_pull);
		}
		if (Input.GetButtonDown("Vertical"))
		{
			canMove = false;

		}
		if (canMove == true)
		{
			transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)

	{
		if (collision.transform.tag == "friend")
		{
			speed = 0f;
			Invoke("Wait_friend", time_friend);
		}
	}

	void Wait_friend()
	{
		speed = 5f;
	}

	void Wait_pull()
	{
		canMove = true;
	}

	void Update()
	{
		MoveControlByTranslate();
	}
}
