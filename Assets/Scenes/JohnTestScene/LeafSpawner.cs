using System;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class LeafSpawner : MonoBehaviour
{
	[SerializeField]
	GameObject waterDropPrefab;

	[SerializeField]
	GameObject caterpillarPrefab;

	[SerializeField]
	GameObject sunshinePrefab;

	public void SpawnWaterDrop()
	{
		var obj = Instantiate(waterDropPrefab, transform.position, transform.rotation);
	}

	public void SpawnCaterpillar()
	{
		var obj = Instantiate(caterpillarPrefab, transform.position, transform.rotation);
	}

	public void SpawnSunShine()
	{
		var obj = Instantiate(sunshinePrefab, transform.position, transform.rotation);
	}
}

