using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class Tree : MonoBehaviour
{
	[SerializeField]
	int gotDew;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.transform.tag)
		{
			case "Caterpillar":
				print("You are ate by Caterpillars");
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
