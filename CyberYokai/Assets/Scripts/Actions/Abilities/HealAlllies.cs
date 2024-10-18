using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAlllies : Ability, IDamageSource
{
	HeroBehaviour target;
	protected override IEnumerator AbilitySetup()
	{
		yield return new WaitUntil(() => AbilityUtils.UserSelection.GetEntity(out target) != null);

		PlayAbility();
	}

	protected override IEnumerator PlayOutAbility()
	{
		yield return new WaitForSeconds(1);

		List<HeroBehaviour> affectedHeroes = AbilityUtils.Selection.GetRowByTarget<HeroBehaviour>(target);

		foreach (var hero in affectedHeroes)
		{
			hero.GetComponent<StatManager>().TakeDamage(-3, this);
		}

		FinishAbility();
	}

	protected override IEnumerator CompleteAbility()
	{
		yield return base.CompleteAbility();

		target = null;
	}
}
