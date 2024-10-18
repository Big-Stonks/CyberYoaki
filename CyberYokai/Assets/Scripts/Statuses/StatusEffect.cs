using System;
using UnityEngine;

/// A group of interfaces that will add more functionality to the status.
#region Status Components

/// Interface that allows the status to be consumed.
/// Used for stuff such as marking an enemy and then consuming him when he's attacked.
public interface IStatusConsumable
{
	public void Consume();

	/// <summary>
	/// Under what condition is the status consumed? i.e when enemy is attacked.
	/// </summary>
	public void SetupConsumeCondition();
}
#endregion

/// <summary>
/// Base class for handling status effect logic.
/// </summary>
public class StatusEffect
{
	public int duration;
	public StatManager target;
	public Ability sourceAbility;

	/// <summary>
	/// Called when status is applied. 
	/// </summary>
	public virtual void OnApply() { }
	/// <summary>
	/// Called on each round start.
	/// </summary>
	public virtual void OnTick() { }
	/// <summary>
	/// Called when status is removed.
	/// </summary>
	public virtual void OnRemove() { }
}

public class DoT : StatusEffect, IDamageSource
{
	public int tickDmg;

	public override void OnTick()
	{
		target.TakeDamage(tickDmg, this);
	}
}

public class Vulnerable : StatusEffect
{
	public float amount = 50;

	public void VulnerabilityCalculation(ref int dmg)
	{
		dmg = Mathf.RoundToInt(dmg * ((amount + 100) / 100f));
	}

	public override void OnApply()
	{
		target.takeDamageModification += VulnerabilityCalculation;
	}
	public override void OnRemove()
	{
		target.takeDamageModification -= VulnerabilityCalculation;
	}
}