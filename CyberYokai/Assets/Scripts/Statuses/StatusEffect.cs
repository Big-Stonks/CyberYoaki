using System;
using UnityEngine;

public interface IStatusConsumable
{
	public void Consume();
	public void SetupConsumeCondition();
}

public class StatusEffect
{
	public int duration;
	public StatManager target;
	public Ability sourceAbility;

	public virtual void OnApply() { }
	public virtual void OnTick() { }
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