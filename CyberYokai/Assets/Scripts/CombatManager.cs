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

	public void Update()
	{

	}

	public void SetCurrentState(CombatState newState)
	{
		currentState = newState;
	}

}
