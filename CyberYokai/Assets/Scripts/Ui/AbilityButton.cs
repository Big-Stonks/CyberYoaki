using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
	private Ability _ability;

	[SerializeField] private TextMeshProUGUI _abilityNameText;

	/// <summary>
	/// Called when a character is selected in order to properly set data for his abilities.
	/// </summary>
	/// <param name="ability">Which ability will be set on this button.</param>
	public void SetAbility(Ability ability)
	{
		_ability = ability;

		_abilityNameText.text = ability.ToString();
	}
	/// <summary>
	/// Called when button is clicked.
	/// </summary>
	public void SelectAbility()
	{
		PlayerController.instance.SelectAbility(_ability);
	}
}
