using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpillar : AutoRelease
{
	[SerializeField]
	float speed = 0.5f;

	Rigidbody2D body;

	bool dir;

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	bool IsOnGround()
	{
		Physics2D.queriesStartInColliders = false;
		if (transform.up.y > 0)
		{
			dir = true;
			Debug.DrawRay(transform.position, -transform.up * 0.12f, Color.red);
			return Physics2D.Raycast(transform.position, -transform.up, 0.12f).transform != null;
		}
		else
		{
			dir = false;
			Debug.DrawRay(transform.position, transform.up * 0.12f, Color.red);
			return Physics2D.Raycast(transform.position, transform.up, 0.12f).transform != null;
		}
	}

	void MoveForward()
	{
		if (dir)
			body.velocity = transform.right * speed;
		else
			body.velocity = -transform.right * speed;
	}

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (!IsOnGround()) return;

		MoveForward();
	}
}
