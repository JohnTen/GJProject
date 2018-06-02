using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtility;

public class FriendTree : MonoBehaviour
{
	[SerializeField]
	int dewCount;

	[SerializeField]
	Faction faction;

	[SerializeField]
	Text dewCounter;

	[SerializeField]
	float fadeDuraition;

	[SerializeField]
	CanvasGroup cgroup;

	Timer showTimer;

	public Faction Faction
	{
		get { return faction; }
	}

	public void GiveDew()
	{
		dewCount++;
		dewCounter.text = $"x {dewCount}";
		showTimer.Start(2);
		StartCoroutine(FadeInOut(true));

		if (dewCount >= 10)
		{
			Win();
		}
	}

	void Win()
	{
		// TODO:
	}

	void Fade_out()
	{
		StartCoroutine(FadeInOut(false));
	}

	IEnumerator FadeInOut(bool fadein)
	{
		float target = fadein ? 1 : 0;
		float start = fadein ? 0 : 1;
		float time = 0;

		while (time < fadeDuraition)
		{
			cgroup.alpha = Mathf.Lerp(start, target, time / fadeDuraition);
			time += Time.deltaTime;
			print(time);
			yield return null;
		}
		cgroup.alpha = target;
	}

	private void Awake()
	{
		showTimer = new Timer();
		showTimer.OnTimeOut += Fade_out;
	}
}
