using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
	[SerializeField]
	Faction faction = Faction.Default;

	[SerializeField]
	SpriteRenderer renderer;

	[SerializeField]
	Sprite defaultSprite;

	public Faction Faction
	{
		get { return faction; }
		set
		{
			faction = value;
			if (faction.groundSprite != null)
				renderer.sprite = faction.groundSprite;
			else
				renderer.sprite = defaultSprite;
		}
	}

	[SerializeField]
	private int caughtWater;
	public int CaughtWater
	{
		get { return caughtWater; }
		set
		{
			caughtWater = value;
			if (caughtWater < 2)
				return;

			caughtWater = 0;
			this.Faction = Faction.Default;
		}
	}

	public bool Transfering { get; set; }
	public bool Rooted { get; set; }

	public void TransformTo(Faction faction)
	{
		if (Transfering) return;
		StartCoroutine(TransferTo(faction));
	}

	IEnumerator TransferTo(Faction faction)
	{
		Transfering = true;
		if (this.faction.ID != faction.ID)
		{
			if (this.faction.ID != Faction.Default.ID)
			{
				yield return new WaitForSeconds(2);
				this.Faction = Faction.Default;
				CaughtWater = 0;
			}

			if (faction.ID != Faction.Default.ID)
			{
				yield return new WaitForSeconds(2);
				this.Faction = faction;
				CaughtWater = 0;
			}
		}
		Transfering = false;
	}
}
