using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caterpillar : AutoRelease
{
	[SerializeField]
	float speed = 0.5f;

	Rigidbody2D body;

	bool dir;

	public float Speed
	{
		get { return speed; }
		set { speed = value; }
	}

	bool IsOnGround()
	{
		Physics2D.queriesStartInColliders = false;
		if (transform.up.y > 0)
		{
			dir = true;
			transform.localScale = Vector3.one;
			Debug.DrawRay(transform.position, -transform.up * 0.25f, Color.red);
			return Physics2D.Raycast(transform.position, -transform.up, 0.25f).transform != null;
		}
		else
		{
			dir = false;
			transform.localScale = new Vector3(1, -1, 1);
			Debug.DrawRay(transform.position, transform.up * 0.25f, Color.red);
			return Physics2D.Raycast(transform.position, transform.up, 0.25f).transform != null;
		}
	}

	void MoveForward()
	{
		body.velocity = transform.right * speed;
	}

	public void ResetReleaseTime(float time)
	{
		releaseTim.Start(time);
	}

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (!IsOnGround()) return;

		MoveForward();
	}

	private void Update()
	{
		var hits = Physics2D.RaycastAll(transform.position, Vector2.down, 2f);
		GroundBlock block = null;
		foreach (var h in hits)
		{
			block = h.transform.GetComponent<GroundBlock>();
			if (block != null) break;
		}

		if (block == null || block.Faction.ID == Faction.Default.ID)
			return;

		block.Faction = Faction.Default;
		ReleaseSelf();
	}
}
