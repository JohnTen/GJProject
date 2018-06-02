using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	[System.Serializable]
	struct SpawnObject
	{
		public GameObject obj;
		public AnimationCurve spawnFreq;
	}

	[SerializeField]
	SpawnObject[] spawnObjects;

	[SerializeField]
	float spawnHeight = 1;

	[SerializeField]
	bool spawning;

	[SerializeField]
	float PassedTime;

	public bool Spawning
	{
		get { return spawning; }
		set { spawning = value; }
	}
	
	void Spawn(int index)
	{
		if (index < 0 || index >= spawnObjects.Length)
			return;

		var obj = spawnObjects[index];
		var freq = obj.spawnFreq.Evaluate(PassedTime);

		if (Random.value > freq * Time.deltaTime) return;

		var scenebound = SceneValues.GetSceneBound();
		var randomX = Random.Range(scenebound.min.x, scenebound.max.x);
		var point = new Vector2(randomX, scenebound.max.y + spawnHeight);

		var spawn = ObjectPool.Acquire(obj.obj);
		spawn.transform.position = point;
	}
	private void Update()
	{
		if (!spawning) return;
		for (int i = 0; i < spawnObjects.Length; i ++)
		{
			Spawn(i);
		}
	}

}
