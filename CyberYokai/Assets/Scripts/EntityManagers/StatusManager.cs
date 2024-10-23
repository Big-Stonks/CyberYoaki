using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffectHandler
{
	public StatusEffect status;
	public int turnsRemaining;
}

/// <summary>
/// Class for managing all the statuses on the entity
/// </summary>
public class StatusManager : MonoBehaviour
{
	public List<StatusEffectHandler> statusEffects;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TickStatuses();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			ApplyStatus(new Vulnerable() { duration = 5, target = GetComponent<StatManager>(), sourceAbility = FindFirstObjectByType<BasicAbility_Fireball>() });
		}
	}

	public void ApplyStatus(StatusEffect status)
	{
		if (statusEffects.Exists(seh => seh.status.GetType() == status.GetType()))
			return;

		// Create a new handler class to add to list
		StatusEffectHandler seh = new StatusEffectHandler()
		{
			status = status,
			turnsRemaining = status.duration
		};

		seh.status.OnApply();
		statusEffects.Add(seh);
		Debug.Log(status + " applied");
	}
	public void TickStatuses()
	{
		foreach (var status in statusEffects)
		{
			status.status.OnTick();
			status.turnsRemaining--;
		}

		statusEffects.ForEach(seh => { if (seh.turnsRemaining <= 0) seh.status.OnRemove(); });
		statusEffects.RemoveAll(sef => sef.turnsRemaining <= 0);
	}
	public void RemoveStatus(StatusEffect status)
	{
		status.OnRemove();
		statusEffects.Remove(statusEffects.Find(seh => seh.status == status));
	}
}
