using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityButton : MonoBehaviour
{
	private Ability _ability;

	[SerializeField] private TextMeshProUGUI _abilityNameText;

	public void SetAbility(Ability ability)
	{
		_ability = ability;

		_abilityNameText.text = ability.ToString();
	}
	public void SelectAbility()
	{
		PlayerController.instance.SelectAbility(_ability);
	}
	public void ResetAbility()
	{
		
	}
}
