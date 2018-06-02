using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour {

	/// <summary>
	/// 摧毁事件
	/// </summary>
	public Action Destroyed;

	public virtual void Initialize()
	{
		// Destroyed = null;
	}

	public virtual void ReleaseSelf() {
		// 调用摧毁事件
		Destroyed?.Invoke();
		Destroyed = null;
		ObjectPool.Release(this);
	}
}
