using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityUtility;

public class GetTile : MonoBehaviour
{
	[SerializeField]
	Tilemap map;

	[SerializeField]
	Transform detectPoint;

	private void Start()
	{
		print($"GetUsedTilesCount: {map.GetUsedTilesCount()}");
	}

	private void Update()
	{
		print($"HasTile({detectPoint.position.ToVector3Int()}): {map.HasTile(detectPoint.position.ToVector3Int())}");
	}
}
