using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

/// <summary>
/// Interface that declares a class as a damage source.
/// Made to easily group classes like abilities, passives, status effects, etc.
/// </summary>
public interface IDamageSource
{

}
public interface IStatModSource
{

}

public class Stat
{
	public virtual void Setup() { }
}
[System.Serializable]
public class Stat_CurrentMax : Stat
{
	public int current;

	public int _base;
	public int total => _base;
	public List<StatModification> modifications;

	public Slider bar;

	public Action<int> onReduce;
	public Action<int> onRefill;

	public Action onMin;
	public Action onMax;

	public override void Setup()
	{
		if (bar != null)
		{
			bar.maxValue = total;
			bar.value = current;
		}
	}
	public void Reduce(int amount)
	{
		current -= amount;
		onReduce?.Invoke(amount);

		if (bar != null)
		{
			bar.value = current;
		}

		if (current <= 0)
		{
			onMin?.Invoke();
		}
	}
	public void Refill(int amount)
	{
		current += amount;
		onRefill?.Invoke(amount);

		if (bar != null)
		{
			bar.value = current;
		}

		if (current >= total)
		{
			onMax?.Invoke();
		}
	}
}
[System.Serializable]
public class Stat_Constant : Stat
{
	public int _base;
	public int total => _base;
	public List<StatModification> modifications;

	public Action onSetup;
	public override void Setup()
	{
		onSetup?.Invoke();
	}
}

public class StatModification
{
	public bool isPercent;
	public int amount;
	public IStatModSource source;
}

/// Some random custom functionality
public delegate void ActionRef<T1>(ref T1 t1);

/// <summary>
/// Base class to declare entity stats data.
/// </summary>
public class StatManager : MonoBehaviour
{
	public Stat_CurrentMax health;
	public Stat_CurrentMax mana;
	public Stat_CurrentMax energy;

	public Stat_Constant damage;
	public Stat_Constant durability;

	public RotationConstraint constraint;

	/// This handles how the damage received is adjusted.
	/// i.e when vulnerability is applied, it adds to this.
	public ActionRef<int> takeDamageModification;
	public Action<IDamageSource, StatManager> onTakeDamage;

	public void Start()
	{
		if (constraint != null)
		{
			constraint.AddSource(new ConstraintSource() { sourceTransform = Camera.main.transform, weight = 1 });
		}

		durability.onSetup += () =>
		{
			takeDamageModification += (ref int i) =>
			{
				float durabilityMultiplier = 1 - (durability.total / 100f);
				int finalDamage = Mathf.RoundToInt(i * durabilityMultiplier);
				i = finalDamage;

				Debug.Log($"Durability adjusted damage by a multiplier of {durabilityMultiplier}. Total damage: {finalDamage}");
			};
		};
		health.onReduce += (i) =>
		{
			if (health.current <= 0)
				Destroy(gameObject);
		};

		health.Setup();
		mana.Setup();
		durability.Setup();
	}

	public void TakeDamage(int amount, IDamageSource source)
	{
		/// Apply damage modifications before dealing the damage
		takeDamageModification?.Invoke(ref amount);

		health.Reduce(amount);

		onTakeDamage?.Invoke(source, this);

		Debug.Log($"{name} took {amount} damage from {source}");
	}
}
