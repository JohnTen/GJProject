using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Sunshine : MonoBehaviour
{
	[SerializeField]
	float speed;

	Rigidbody2D body;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		body.velocity = Vector3.down * speed;
	}
}
