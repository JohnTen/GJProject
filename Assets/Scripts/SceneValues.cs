using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class SceneValues
{
	public static Bounds GetSceneBound()
	{
		var maxEdge = Camera.main.ViewportToWorldPoint(Vector2.one);
		var minEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);

		return new Bounds(Vector3.zero, maxEdge - minEdge);
	}

	public static bool IsWithInScene(Bounds bound)
	{
		return bound.IsWithInBound(GetSceneBound());
	}
}
