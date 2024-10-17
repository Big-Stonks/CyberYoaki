using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
	public int currentHealth;
	public int maxHealth;

	public Slider healthBar;

	public void Start()
	{
		currentHealth = maxHealth;

		if (healthBar != null)
		{
			healthBar.maxValue = maxHealth;
			healthBar.value = currentHealth;
			healthBar.GetComponent<RotationConstraint>().AddSource(new ConstraintSource() { sourceTransform = Camera.main.transform, weight = 1 });
		}
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		UpdateUi();

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
