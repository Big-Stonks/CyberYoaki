using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Basic class for storing logic for Abilities.
/// </summary>
public class Ability : MonoBehaviour
{
	protected PlayableEntity caster;

	public Action onFinished;

	public bool inCast;

	public void UseAbility(PlayableEntity caster)
	{
		this.caster = caster;
		StartCoroutine(AbilitySetup());
	}
	public void PlayAbility()
	{
		StartCoroutine(PlayOutAbility());
	}
	public void FinishAbility()
	{
		StartCoroutine(CompleteAbility());
	}

	protected virtual IEnumerator AbilitySetup()
	{
		yield return null;
	}
	protected virtual IEnumerator PlayOutAbility()
	{
		yield return null;
	}
	protected virtual IEnumerator CompleteAbility()
	{
		Debug.Log("ABILITY DONE DELAY STARTED");
		yield return new WaitForSeconds(1);

		onFinished?.Invoke();
		onFinished = null;
		inCast = false;

		Debug.Log("ABILITY DONE UWU");
	}
}
