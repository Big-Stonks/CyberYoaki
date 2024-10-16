using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic class for storing logic for Abilities.
/// </summary>
public class Ability : MonoBehaviour
{
	public List<AbilityCast> casts;

	public Action onAbilityFinished;

	protected PlayableEntity caster;

	public void UseAbility(PlayableEntity caster)
	{
		this.caster = caster;
	}

	public virtual void AbilityAction() { }

	public void FinishAbility()
	{
		onAbilityFinished?.Invoke();
		onAbilityFinished = null;
	}
}

[System.Serializable]
public class AbilityCast
{
	public enum CastType { SingleTarget, AoE, Adjecent, Row }
	public enum CastTargeting { SelfOnly, Ally, Enemy }
}