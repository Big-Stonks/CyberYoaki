using System.Collections.Generic;
using UnityEngine;

public enum PlayerTurnState { Selection, AbilitySelected, AbilityAction }

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

	// Game Control
	PlayerTurnState _currentTurnState;
	PlayerTurnState CurrentTurnState
	{
		get
		{
			return _currentTurnState;
		}
		set
		{
			_currentTurnState = value;
		}
	}

	PlayableEntity _selectedCharacter;
	PlayableEntity SelectedCharacter
	{
		/// Just a classic Property.
		/// Instead of just assigning the value to the variable, we're gonna do some
		/// more stuff before or after it's assigned (check down)
		get
		{
			return _selectedCharacter;
		}
		set
		{
			/// Check if character is selected or deselected
			if (value != null)
			{
				/// Check if we chose different character that the one currently selected
				if (value != _selectedCharacter)
					SetupCombatHud(value as HeroBehaviour);
			}
			else
			{
				CloseCombatHud();
			}

			/// Set character after setting up hud
			_selectedCharacter = value;
		}
	}

	Ability _selectedAbility;

	RaycastHit hitInfo;

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
		if (Physics.Raycast(ray, out hitInfo, 1000f, LayerMask.GetMask("Entity")))
		{
			/// Check if the object is behaviour entity (character, enemy, minion, etc.)
			if (hitInfo.collider.GetComponent<EntityBehaviour>() != null)
			{
				/// Check if it's playable entity in order to set it up for ui and other stuff
				if (hitInfo.collider.GetComponent<PlayableEntity>() != null)
				{
					if (Input.GetMouseButtonDown(0))
					{
						SelectedCharacter = hitInfo.collider.GetComponent<PlayableEntity>();
					}
				}
			}
		}

		switch (CurrentTurnState)
		{
			case PlayerTurnState.Selection:
				{

				}
				break;
			case PlayerTurnState.AbilitySelected:
				{

				}
				break;
			case PlayerTurnState.AbilityAction:
				{

				}
				break;
			default:
				break;
		}
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

	public void SelectAbility(Ability ability)
	{

	}

	public void ChangeState(PlayerTurnState newState)
	{

	}
}
