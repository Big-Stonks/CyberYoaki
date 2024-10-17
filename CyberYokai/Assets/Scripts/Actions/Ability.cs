using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic class for storing logic for Abilities.
/// </summary>
public class Ability : MonoBehaviour
{
	protected PlayableEntity caster;

	public void UseAbility(PlayableEntity caster)
	{
		this.caster = caster;
		StartCoroutine(AbilitySetup());
	}
	public void PlayAbility()
	{
		StartCoroutine(PlayOutAbility());
	}

	public virtual IEnumerator AbilitySetup()
	{
		yield return null;
	}
	public virtual IEnumerator PlayOutAbility()
	{
		yield return null;
	}

	public virtual void FinishAbility()
	{
		PlayerController.instance.canInteract = true;
	}
}
