using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		var rigid = collision.transform.GetComponent<Rigidbody2D>();
		rigid.drag = 1;
		rigid.velocity *= 1f;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		var rigid = collision.transform.GetComponent<Rigidbody2D>();
		rigid.drag = 0;
	}
}
