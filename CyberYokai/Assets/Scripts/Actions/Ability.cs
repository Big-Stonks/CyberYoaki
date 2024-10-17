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

	/// <summary>
	/// Called when player presses on the ability ui button.
	/// Used to setup all the data such as selections.
	/// </summary>
	/// <param name="caster">Who casted the ability?</param>
	public void UseAbility(PlayableEntity caster)
	{
		this.caster = caster;
		StartCoroutine(AbilitySetup());
	}
	/// <summary>
	/// Called when ability stars its action.
	/// </summary>
	public void PlayAbility()
	{
		StartCoroutine(PlayOutAbility());
	}
	/// <summary>
	/// Called 
	/// </summary>
	public void FinishAbility()
	{
		StartCoroutine(CompleteAbility());
	}

	/// <summary>
	/// Override this to have custom setup logic.
	/// </summary>
	protected virtual IEnumerator AbilitySetup()
	{
		yield return null;
	}
	/// <summary>
	/// Override this to have custom action logic.
	/// </summary>
	protected virtual IEnumerator PlayOutAbility()
	{
		yield return null;
	}
	/// <summary>
	/// Override this to declare what happens when the ability finishes.
	/// It also notifies others that the ability is finished.
	/// Has a 1 second delay in order to allow for follow ups and other calculations.
	/// </summary>
	protected virtual IEnumerator CompleteAbility()
	{
		yield return new WaitForSeconds(1);

		onFinished?.Invoke();
		onFinished = null;
		inCast = false;
	}
}
