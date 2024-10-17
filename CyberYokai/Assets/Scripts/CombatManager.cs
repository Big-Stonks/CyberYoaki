using System.Collections.Generic;
using UnityEngine;

public enum CombatState { PlayerTurn, EnemyTurn }

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

	public void Update()
	{
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

}
