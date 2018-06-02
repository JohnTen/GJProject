using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PCTree : MonoBehaviour
{
	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float transferDewDelay = 3.0f;

	[SerializeField]
	private float pullDelay = 1f;

	[SerializeField]
	private float pushDelay = 0.5f;

	[SerializeField]
	private float waterValue;

	[SerializeField]
	private float waterDecRate = 0.5f;

	[SerializeField]
	private float waterIncRate = 3f;

	[SerializeField]
	private Animator animator;

	[SerializeField]
	private Collider2D sceneBoundCollider;

	[SerializeField]
	private UnityEvent diedByDry;

	[SerializeField]
	private UnityEvent diedByCaterpillar;

	[SerializeField]
	private Slider slider;

	private bool canMove = true;
	private bool rooted = false;
	private bool caughtDew = false;

	void MoveControlByTranslate()
	{
		if (Input.GetButtonDown("Jump") && canMove)
		{
			canMove = false;
			if (rooted)
			{
				animator.SetBool("Root", false);
				Invoke("Wait_pull", pullDelay);
			}
			else
			{
				animator.SetBool("Root", true);
				Invoke("Wait_push", pushDelay);
			}
		}

		if (!rooted && canMove)
		{
			var move = Input.GetAxisRaw("Horizontal");

			animator.SetFloat("Move", move);

			transform.Translate(move * Time.deltaTime * speed, 0, 0);
			ClampToScene();
		}
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
	}

	void Update()
	{
		MoveControlByTranslate();

		if (rooted)
		{
			waterValue += waterIncRate * Time.deltaTime;
		}
		else
		{
			waterValue -= waterDecRate * Time.deltaTime;
		}

		if (waterValue <= 0)
		{
			animator.SetBool("Dried", true);
			diedByDry?.Invoke();
		}

		waterValue = Mathf.Clamp01(waterValue);

		slider.value = waterValue;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.transform.tag)
		{
			case "Caterpillar":
				print("You are ate by Caterpillars");
				animator.SetBool("Ate", true);

				var obj = collision.transform.GetComponent<SpawnableObject>();
				if (obj == null)
				{
					obj = collision.transform.parent.GetComponent<SpawnableObject>();
				}

				obj.ReleaseSelf();
				diedByCaterpillar?.Invoke();
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
				break;

			case "Friend":
				if (!caughtDew)
				{
					print("You have no dew to give");
					break;
				}
				canMove = false;
				Invoke("Wait_friend", transferDewDelay);
				print("Give dew to friend");
				break;
		}
	}
}
