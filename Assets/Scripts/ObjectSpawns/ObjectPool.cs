using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	/// <summary>
	/// For pooling the GameObjects
	/// </summary>
	[NonSerialized]
	private readonly Dictionary<GameObject, Queue<GameObject>> pool =
		new Dictionary<GameObject, Queue<GameObject>>();

	/// <summary>
	/// For tracking the GameObjects which created by <see cref="ObjectPool"/>
	/// to prevent releasing an object that is not created from this pool.
	/// </summary>
	[NonSerialized]
	private readonly Dictionary<GameObject, GameObject> prefabMap =
		new Dictionary<GameObject, GameObject>();

	static ObjectPool instance;

	/// <summary>
	/// Create a instance of <see cref="ObjectPool"/> 
	/// if it isn't already created.
	/// </summary>
	public static void CreateInstance()
	{
		if (instance == null)
		{
			instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
		}
	}

	/// <summary>
	/// Acquire a object from pool by the prefab of the object.
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public static GameObject Acquire(GameObject prefab)
	{
		CreateInstance();
		return instance.AcquireInstance(prefab);
	}

	/// <summary>
	/// Acquire a Component(with GameObject) from pool by the prefab of the Component.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public static T Acquire<T>(T prefab) where T : Component
	{
		if (prefab == null) return Acquire(prefab);
		return Acquire(prefab.gameObject).GetComponent<T>();
	}

	/// <summary>
	/// Release a object to pool.
	/// </summary>
	/// <param name="obj"></param>
	public static void Release(GameObject obj)
	{
		CreateInstance();
		instance.ReleaseInstance(obj);
	}

	/// <summary>
	/// Release a component(with GameObject) to pool.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="prefab"></param>
	public static void Release<T>(T prefab) where T : Component
	{
		if (prefab == null) return;
		Release(prefab.gameObject);
	}

	/// <summary>
	/// Backing method of <see cref="Acquire(GameObject)"/>
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	private GameObject AcquireInstance(GameObject prefab)
	{
		if (prefab == null)
			throw new ArgumentNullException("perfab");

		Queue<GameObject> queue;

		if (!pool.TryGetValue(prefab, out queue))
			pool.Add(prefab, queue = new Queue<GameObject>());

		GameObject ret;
		if (queue.Count > 0)
		{
			ret = queue.Dequeue();
			ret.SetActive(true);
			ret.transform.parent = null;
		} else
		{
			ret = Instantiate(prefab);
			prefabMap[ret] = prefab;
		}

		return ret;
	}

	/// <summary>
	/// Backing method of <see cref="Release(GameObject)"/>
	/// </summary>
	/// <param name="obj"></param>
	private void ReleaseInstance(GameObject obj)
	{
		if (obj == null)
			throw new ArgumentNullException("obj");

		GameObject prefab;
		if (!prefabMap.TryGetValue(obj, out prefab))
			throw new ArgumentException("The object isn't created from this pool.");
			

		Queue<GameObject> queue;
		if (!pool.TryGetValue(prefab, out queue))
		{
			Debug.LogWarning("Releasing a object with no stack trace, please do some checking.");
			pool.Add(prefab, queue = new Queue<GameObject>());
		}

		obj.transform.SetParent(transform);
		obj.SetActive(false);
		queue.Enqueue(obj);
	}
}
