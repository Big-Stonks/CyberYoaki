using System;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { PlayerTurn, EnemyTurn }

/// <summary>
/// Class used for managing and tracking combat state and provides utilities to more easily manipulate it.
/// It tracks data bout entity positions, who's turn it is, ability queueing, and so on.
/// </summary>
public class CombatManager : MonoBehaviour
{
	public static CombatManager instance;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	[SerializeField] CombatState currentState;

	public List<EntitySpot> spots;

	public List<Ability> queuedAbilities;

	/// <summary>
	/// This is called whenever ANY ability finishes casting.
	/// </summary>
	public Action<Ability> onAbilityFinished;

	public void Update()
	{
		/// While there are abilities to be used, play them one by one.
		/// Also disable/enable player input based on that.
		PlayerController.instance.canInteract = queuedAbilities.Count == 0;

		if (queuedAbilities.Count > 0)
		{
			if (!queuedAbilities[0].inCast)
			{
				queuedAbilities[0].inCast = true;
				queuedAbilities[0].PlayAbility();
				queuedAbilities[0].onFinished += () =>
				{
					queuedAbilities.RemoveAt(0);
				};
			}
		}
	}

	public static void QueueAbility(Ability ability)
	{
		instance.queuedAbilities.Add(ability);
	}

	public void SetCurrentState(CombatState newState)
	{
		currentState = newState;
	}

	public void CallOnAbilityFinished(Ability ability)
	{
		onAbilityFinished?.Invoke(ability);
		onAbilityFinished = null;
	}
}
