using System;
using System.Collections.Generic;
using UnityEngine;


public class AddSceneBound : MonoBehaviour
{
	private void Update()
	{
		var maxEdge = Camera.main.ViewportToWorldPoint(Vector2.one);
		var minEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);

		Bounds bound = new Bounds(Vector3.zero, maxEdge - minEdge);

		Debug.DrawLine(bound.center, bound.max);
		Debug.DrawLine(bound.center, bound.min);
	}
}
