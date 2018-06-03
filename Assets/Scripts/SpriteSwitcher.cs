using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSwitcher : MonoBehaviour
{
	[SerializeField]
	Sprite[] sprites;

	[SerializeField]
	float fadeDuraition = 0.3f;

	SpriteRenderer renderer;

	public void SwitchTo(int index)
	{
		renderer.sprite = sprites[index];
	}

	public void Fadein()
	{
		StopAllCoroutines();
		StartCoroutine(FadeInOut(true));
	}

	public void Fadeout()
	{
		StopAllCoroutines();
		StartCoroutine(FadeInOut(false));
	}

	IEnumerator FadeInOut(bool fadein)
	{
		float target = fadein ? 1 : 0;
		float start = fadein ? 0 : 1;
		var color = renderer.color;
		float time = 0;
		
		while (time < fadeDuraition)
		{
			color.a = Mathf.Lerp(start, target, time / fadeDuraition);
			renderer.color = color;
			time += Time.deltaTime;

			yield return null;
		}
		color.a = target;
		renderer.color = color;
	}

	private void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
	}
}
