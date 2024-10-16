using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState { PlayerTurn, EnemyTurn }

public class CombatManager : MonoBehaviour
{
	[SerializeField] CombatState currentState;

	public void Update()
	{

	}

	public void SetCurrentState(CombatState newState)
	{
		currentState = newState;
	}
}
