using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch(collision.transform.tag)
		{
			case "Caterpillar":
			var obj = collision.transform.GetComponent<AutoRelease>();
			if (obj == null)
			{
				obj = collision.transform.parent.GetComponent<AutoRelease>();
			}

				obj.ReleaseTime = 3;
			break;

			case "WaterDrop":
			collision.transform.GetComponent<SpawnableObject>().ReleaseSelf();
			break;

			case "Sunshine":
			collision.transform.GetComponent<SpawnableObject>().ReleaseSelf();
			break;
		}
	}
}
