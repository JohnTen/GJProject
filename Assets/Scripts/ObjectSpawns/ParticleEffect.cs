using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffect : AutoRelease
{
	protected void Awake()
	{
		releaseTime = GetComponent<ParticleSystem>().main.duration;
	}
}
