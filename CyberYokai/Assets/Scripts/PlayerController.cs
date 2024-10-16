using System.Collections.Generic;
using UnityEngine;

public enum PlayerTurnState { Selection, AbilitySelected, AbilityCast }

public class PlayerController : MonoBehaviour
{
	/// <summary>
	/// Classic Singleton
	/// </summary>
	public static PlayerController instance;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	/// References
	[SerializeField] private GameObject _combatHud;
	[SerializeField] private List<AbilityButton> _abilityButtons;

	// Game Controls
	/// <summary>
	/// This variable represents the current action state of the player.
	/// Selection - Player can select characters and abilities, once the ability is selected this changes to next.
	/// AbilitySelected - Player needs to select a target to use ability then this changes to next.
	/// AbilityCast - This is here to prevent player from selecting stuff until the current
	///				  ability finishes casting. After ability finishes, this changes back to Selection.
	/// </summary>
	[SerializeField] PlayerTurnState _currentTurnState;

	[SerializeField] PlayableEntity _selectedCharacter;
	[SerializeField] Ability _selectedAbility;
	[SerializeField] EnemyBehaviour _selectedEnemy;

	// Private conditional variables
	RaycastHit _hitInfo;
	bool _hasHit => _hitInfo.collider != null;
	bool _stateSet;

	private void Start()
	{
		CloseCombatHud();
	}

	public void Update()
	{
		/// Create a ray and if it hits something, put that value in hitInfo var.
		/// We're not checking here by layer, rather by component that the object has.
		/// i.e - if you want enemy, check if the object has EnemyBehaviour script rather than 'Enemy' layer.
		/// We still have to have a 'Entity' layer on anything we want to be interactable, we
		/// just differentiate them by checking on components
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(ray, out _hitInfo, 1000f, LayerMask.GetMask("Entity"));

		switch (_currentTurnState)
		{
			case PlayerTurnState.Selection:
				{
					if (_hasHit)
					{
						/// Check if the object is behaviour entity (character, enemy, minion, etc.)
						if (_hitInfo.collider.GetComponent<EntityBehaviour>() != null)
						{
							/// Check if it's playable entity in order to set it up for ui and other stuff
							if (_hitInfo.collider.GetComponent<PlayableEntity>() != null)
							{
								if (Input.GetMouseButtonDown(0))
								{
									SelectCharacter(_hitInfo.collider.GetComponent<HeroBehaviour>());
								}
							}
						}
					}
				}
				break;
			case PlayerTurnState.AbilitySelected:
				{
					if (_hasHit)
					{
						if (_hitInfo.collider.GetComponent<EntityBehaviour>() != null)
						{
							if (_hitInfo.collider.GetComponent<EnemyBehaviour>() != null)
							{
								if (Input.GetMouseButtonDown(0))
								{
									SelectEnemy(_hitInfo.collider.GetComponent<EnemyBehaviour>());
								}
							}
						}
					}
				}
				break;
			case PlayerTurnState.AbilityCast:
				{
					if (!_stateSet)
					{
						_stateSet = true;

						_selectedAbility.onAbilityFinished += FinishCast;
						_selectedAbility.UseAbility(_selectedCharacter);
					}
				}
				break;
			default:
				break;
		}
	}

	public void SelectCharacter(HeroBehaviour character)
	{
		_selectedCharacter = character;
		SetupCombatHud(character);
	}
	public void SelectAbility(Ability ability)
	{
		_selectedAbility = ability;
		ChangeState(PlayerTurnState.AbilitySelected);
	}
	public void SelectEnemy(EnemyBehaviour enemy)
	{
		_selectedEnemy = enemy;
		ChangeState(PlayerTurnState.AbilityCast);
		CloseCombatHud();
	}

	public void SetupCombatHud(HeroBehaviour hero)
	{
		_combatHud.SetActive(true);

		foreach (var abilityButton in _abilityButtons)
		{
			abilityButton.gameObject.SetActive(false);
		}
		for (int i = 0; i < hero.abilities.Count; i++)
		{
			_abilityButtons[i].gameObject.SetActive(true);
			_abilityButtons[i].SetAbility(hero.abilities[i]);
		}
	}
	public void CloseCombatHud()
	{
		_combatHud.SetActive(false);
	}

	public void ChangeState(PlayerTurnState newState)
	{
		_currentTurnState = newState;
	}

	public void FinishCast()
	{
		_selectedAbility = null;
		_selectedCharacter = null;
		_selectedEnemy = null;
		_stateSet = false;
		ChangeState(PlayerTurnState.Selection);
	}
}
