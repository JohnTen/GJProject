﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

public class AutoRelease : SpawnableObject
{
	[SerializeField]
	protected float releaseTime;

	Timer releaseTim;

	private void OnEnable()
	{
		Initialize();
	}

	public override void Initialize()
	{
		base.Initialize();
		releaseTim = new Timer();
		releaseTim.OnTimeOut += ReleaseSelf;
		releaseTim.Start(releaseTime);
	}

	public override void ReleaseSelf()
	{
		releaseTim.Dispose();
		base.ReleaseSelf();
	}
}