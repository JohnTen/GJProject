using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpillar : MonoBehaviour
{
	[SerializeField]
	float speed = 0.5f;

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	bool IsOnGround()
	{
		Physics2D.queriesStartInColliders = false;
		Debug.DrawRay(transform.position, -transform.up * 0.12f, Color.red);
		return Physics2D.Raycast(transform.position, -transform.up, 0.12f).transform != null;
	}

	void MoveForward()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}

	private void FixedUpdate()
	{
		if (!IsOnGround()) return;

		MoveForward();
	}
}
