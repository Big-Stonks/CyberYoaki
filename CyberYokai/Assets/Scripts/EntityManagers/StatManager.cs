using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public interface IDamageSource
{

}

public delegate void ActionRef<T1> (ref T1 t1);

/// <summary>
/// Base class to declare entity stats data.
/// </summary>
public class StatManager : MonoBehaviour
{
	public int currentHealth;
	public int maxHealth;

	public Slider healthBar;

	public ActionRef<int> takeDamageModification;
	public Action<IDamageSource, StatManager> onTakeDamage;

	public void Start()
	{
		currentHealth = maxHealth;

		/// If healthbar is present, set it up.
		if (healthBar != null)
		{
			healthBar.maxValue = maxHealth;
			healthBar.value = currentHealth;
			healthBar.GetComponent<RotationConstraint>().AddSource(new ConstraintSource() { sourceTransform = Camera.main.transform, weight = 1 });
		}
	}

	public void TakeDamage(int amount, IDamageSource source)
	{
		takeDamageModification?.Invoke(ref amount);

		currentHealth -= amount;
		UpdateUi();

		Debug.Log(gameObject.name + " took " + amount + " damage from " + source);
		onTakeDamage?.Invoke(source, this);

		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void UpdateUi()
	{
		healthBar.value = currentHealth;
	}
}
