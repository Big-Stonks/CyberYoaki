using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAlllies : Ability
{
	HeroBehaviour target;
	public override IEnumerator AbilitySetup()
	{
		while (target == null)
		{
			target = AbilityUtils.UserSelection.GetEntity<HeroBehaviour>();
			yield return null;
		}

		PlayAbility();
	}
	public override IEnumerator PlayOutAbility()
	{
		yield return new WaitForSeconds(1);

		List<HeroBehaviour> affectedHeroes = AbilityUtils.Selection.GetRowByTarget<HeroBehaviour>(target);

		foreach (var hero in affectedHeroes)
		{
			Debug.Log("HEALED " + hero.gameObject.name);
		}

		FinishAbility();
	}

	public override void FinishAbility()
	{
		target = null;

		base.FinishAbility();
	}
}
