using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
	[SerializeField] PlayableEntity _selectedCharacter;
	[SerializeField] Ability _selectedAbility;

	public RaycastHit _hitInfo;
	public bool _hasHit => _hitInfo.collider != null;
	public bool canInteract;

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

		if (canInteract)
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
	}

	/// <summary>
	/// Called when a character is clicked.
	/// </summary>
	public void SelectCharacter(HeroBehaviour character)
	{
		_selectedCharacter = character;
		SetupCombatHud(character);
	}
	/// <summary>
	/// Called when ability in hud is clicked to set it up for casting.
	/// </summary>
	public void SelectAbility(Ability ability)
	{
		_selectedAbility = ability;
		_selectedAbility.UseAbility(_selectedCharacter);
		canInteract = false;
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
}
