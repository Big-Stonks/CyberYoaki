using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAlllies : Ability
{
	HeroBehaviour target;
	protected override IEnumerator AbilitySetup()
	{
		while (target == null)
		{
			target = AbilityUtils.UserSelection.GetEntity<HeroBehaviour>();
			yield return null;
		}

		PlayAbility();
	}
	protected override IEnumerator PlayOutAbility()
	{
		yield return new WaitForSeconds(1);

		List<HeroBehaviour> affectedHeroes = AbilityUtils.Selection.GetRowByTarget<HeroBehaviour>(target);

		foreach (var hero in affectedHeroes)
		{
			Debug.Log("HEALED " + hero.gameObject.name);
		}

		FinishAbility();
	}

	protected override IEnumerator CompleteAbility()
	{
		yield return base.CompleteAbility();

		target = null;
	}
}
