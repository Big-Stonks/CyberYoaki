using System.Collections;
using UnityEngine;

public class Fireball : Ability
{
	EnemyBehaviour target;
	public Projectile_StraightPath projectilePrefab;

	public override IEnumerator AbilitySetup()
	{
		while (target == null)
		{
			target = AbilityUtils.UserSelection.GetEntity<EnemyBehaviour>();
			yield return null;
		}

		PlayAbility();
	}

	public override IEnumerator PlayOutAbility()
	{
		Projectile_StraightPath p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		p.target = target;
		p.speed = 4;
		p.onTargetReached += () =>
		{
			target.GetComponent<StatManager>().TakeDamage(5);
			FinishAbility();
		};

		yield return null;
	}

	public override void FinishAbility()
	{
		target = null;

		base.FinishAbility();
	}
}
