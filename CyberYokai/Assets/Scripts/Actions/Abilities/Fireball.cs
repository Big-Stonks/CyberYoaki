using System.Collections;
using UnityEngine;

public class Fireball : Ability, IDamageSource
{
	EnemyBehaviour target;
	public Projectile_StraightPath projectilePrefab;

	protected override IEnumerator AbilitySetup()
	{
		yield return new WaitUntil(() => AbilityUtils.UserSelection.GetEntity(out target) != null);

		CombatManager.QueueAbility(this);
	}

	protected override IEnumerator PlayOutAbility()
	{
		Projectile_StraightPath p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
		p.target = target;
		p.speed = 4;
		p.onTargetReached += () =>
		{
			/*target.GetComponent<StatusManager>().ApplyStatus(new Vulnerable()
			{
				target = target.GetComponent<StatManager>(),
				duration = 2,
				sourceAbility = this
			});
			target.GetComponent<StatusManager>().ApplyStatus(new DoT()
			{
				target = target.GetComponent<StatManager>(),
				tickDmg = 1,
				duration = 3,
				sourceAbility = this
			});*/
			target.GetComponent<StatManager>().TakeDamage(5, this);

			target.GetComponent<StatusManager>().ApplyStatus(new FireMark()
			{
				target = target.GetComponent<StatManager>(),
				duration = 5,
				sourceAbility = this
			});

			FinishAbility();
		};

		yield return null;
	}

	protected override IEnumerator CompleteAbility()
	{
		yield return base.CompleteAbility();

		target = null;
	}

	public class FireMark : StatusEffect, IStatusConsumable, IDamageSource
	{
		public void Consume()
		{
			target.TakeDamage(3, this);
		}
		public void SetupConsumeCondition()
		{
			target.onTakeDamage += CheckForConsumeCondition;
		}

		public override void OnRemove()
		{
			target.onTakeDamage -= CheckForConsumeCondition;
		}

		public void CheckForConsumeCondition(IDamageSource damageSource, StatManager damagedTarget)
		{
			if (damageSource == sourceAbility as IDamageSource)
			{
				Consume();
				CombatManager.instance.onAbilityFinished += (ability) => damagedTarget.GetComponent<StatusManager>().RemoveStatus(this);
			}
		}
	}
}
