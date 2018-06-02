using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class Tree : MonoBehaviour
{
	Bounds GetSceneBound()
	{
		var maxEdge = Camera.main.ViewportToWorldPoint(Vector2.one);
		var minEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);

		return new Bounds(Vector3.zero, maxEdge - minEdge);
	}

	bool IsWithInScene()
	{
		return GetComponentInChildren<Collider2D>().bounds.IsWithInBound(GetSceneBound());
	}

	private void Update()
	{
		print(IsWithInScene());
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.transform.tag)
		{
			case "Bug":
				print("You are ate by bugs");
				break;
			case "WaterDrop":
				print("You caught a drop of water");
				break;
			case "Sunshine":
				print("You block a ray of sunshine");
				break;
		}
	}
}
