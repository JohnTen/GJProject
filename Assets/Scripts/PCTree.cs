using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityUtility;

[System.Serializable]
public struct Faction
{
	public string name;
	public int ID;
	public string moveAxis;
	public string rootButton;
	public Sprite groundSprite;

	public Faction(string name, int ID, string moveAxis, string rootButton, Sprite sprite)
	{
		this.name = name;
		this.ID = ID;
		this.moveAxis = moveAxis;
		this.rootButton = rootButton;
		this.groundSprite = sprite;
	}

	public static Faction Default = 
		new Faction(
		"Default",
		-1,
		"",
		"",
		null);
}

public class PCTree : MonoBehaviour
{
	[SerializeField]
	Faction faction;

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float transferDewDelay = 3.0f;

	[SerializeField]
	private float pullDelay = 1f;

	[SerializeField]
	private float pushDelay = 0.5f;
	
	[SerializeField]
	private int sunCount = 0;

	[SerializeField]
	private SpriteSwitcher sunpanel;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Collider2D sceneBoundCollider;

	[SerializeField]
	private GroundBlock standingBlock;

	private bool canMove = true;
	private bool rooted = false;
	private bool ate = false;
	private bool caughtDew = false;
	private bool blockPenalty = false;

	private Timer sunTimer;
	private Timer caterpillarTimer;

	public int SunCount
	{
		get { return sunCount; }
		set
		{
			sunCount = value;
			sunpanel.SwitchTo(sunCount);
			sunpanel.Fadein();
			sunTimer.Start(2);
		}
	}

	void Rooting()
	{
		if (ate || !canMove)
			return;

		if (Input.GetButtonDown(faction.rootButton) && canMove)
		{
			if (rooted)
			{
				if (standingBlock.Faction.ID != this.faction.ID)
					return;

				canMove = false;
				standingBlock.Rooted = false;
				animator.SetBool("Root", false);
				Invoke("Wait_pull", pullDelay);
			}
			else
			{
				if (standingBlock.Transfering ||
					standingBlock.Rooted)
					return;
				
				if (standingBlock.Faction.ID != this.faction.ID)
				{
					if (SunCount <= 0)
					{
						SunCount = 0;
						return;
					}

					SunCount--;
				}

				canMove = false;
				standingBlock.Rooted = true;
				animator.SetBool("Root", true);
				Invoke("Wait_push", pushDelay);
			}
		}
	}

	void MoveControlByTranslate()
	{
		if (rooted || ate || !canMove)
			return;

		var move = Input.GetAxisRaw(faction.moveAxis);

		animator.SetFloat("Move", move);

		if (blockPenalty)
			move *= 0.5f;

		transform.Translate(move * Time.deltaTime * speed, 0, 0);
		ClampToScene();
	}

	void ClampToScene()
	{
		var bound = sceneBoundCollider.bounds;
		if (SceneValues.IsWithInScene(bound))
			return;

		var scene = SceneValues.GetSceneBound();

		var diff = bound.max - scene.max;
		if (diff.x > 0)
		{
			transform.position -= Vector3.right * diff.x;
			return;
		}

		diff = bound.min - scene.min;
		if (diff.x < 0)
		{
			transform.position -= Vector3.right * diff.x;
			return;
		}
	}

	void Wait_Caterpillar()
	{
		ate = false;
		animator.SetBool("Ate", false);
	}

	void Wait_friend()
	{
		caughtDew = false;
		canMove = true;
	}

	void Wait_pull()
	{
		rooted = false;
		canMove = true;
	}

	void Wait_push()
	{
		rooted = true;
		canMove = true;
		standingBlock.TransformTo(faction);
	}

	void Hide_sun()
	{
		sunpanel.Fadeout();
	}

	void GetBlock()
	{
		var hits = Physics2D.RaycastAll(transform.position, Vector2.down);
		var block = standingBlock;
		foreach (var h in hits)
		{
			block = h.transform.GetComponent<GroundBlock>();
			if (block != null) break;
		}

		if (block == null || block == standingBlock)
			return;

		blockPenalty = false;
		print("You stand on " + block.name);
		standingBlock = block;
		if (standingBlock.Faction.ID == this.faction.ID ||
			standingBlock.Faction.ID == Faction.Default.ID)
			return;
		
		blockPenalty = true;
		if (!caughtDew) return;
		caughtDew = false;

		standingBlock.CaughtWater++;
		if (standingBlock.Faction.ID == Faction.Default.ID)
			blockPenalty = false;
	}

	private void Awake()
	{
		sunTimer = new Timer();
		sunTimer.OnTimeOut += Hide_sun;

		caterpillarTimer = new Timer();
		caterpillarTimer.OnTimeOut += Wait_Caterpillar;
	}

	void Update()
	{
		GetBlock();
		Rooting();
		MoveControlByTranslate();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.transform.tag)
		{
			case "Caterpillar":
				if (!rooted)
				{
					animator.SetBool("Ate", true);
					ate = true;
					caterpillarTimer.Start(1);
				}

				var obj = collision.transform.GetComponent<SpawnableObject>();
				if (obj == null)
				{
					obj = collision.transform.parent.GetComponent<SpawnableObject>();
				}

				obj.ReleaseSelf();
				break;

			case "WaterDrop":
				if (!caughtDew)
				{
					print("You caught a drop of water");
					caughtDew = true;
				}
				else
				{
					print("You already caught a drop of water");
				}

				collision.transform.GetComponent<SpawnableObject>().ReleaseSelf();
				break;

			case "Sunshine":
				print("You block a ray of sunshine");
				collision.transform.GetComponent<SpawnableObject>().ReleaseSelf();
				SunCount = SunCount >= 3 ? 3 : SunCount + 1;
				break;

			case "Friend":
				var tree = collision.transform.GetComponent<FriendTree>();

				if (tree.Faction.ID != this.faction.ID)
					break;

				if (!caughtDew)
				{
					print("You have no dew to give");
					break;
				}

				tree.GiveDew();
				canMove = false;
				Invoke("Wait_friend", transferDewDelay);
				print("Give dew to friend");
				break;
		}
	}
}
