using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSocietyRise : BasicAbility
{
	public EntityBehaviour minionPrefab;

	EntitySpot spot;
	EntityBehaviour minion;

	protected override IEnumerator AbilitySetup()
	{
		yield return new WaitUntil(() => AbilityUtils.UserSelection.GetSpot(out spot, EntitySpot.SpotSide.Player));
		PlayAbility();
	}

	protected override IEnumerator PlayOutAbility()
	{
		minion = Instantiate(minionPrefab, spot.transform.position, Quaternion.identity);
		minion.spot = spot;
		spot.entityOnThis = minion;
		minion.GetComponent<StatusManager>().ApplyStatus(new MinionGone()
		{
			target = minion.GetComponent<StatManager>(),
			duration = 3,
			sourceAbility = this
		});

		yield return null;
	}

	protected override IEnumerator CompleteAbility()
	{
		yield return base.CompleteAbility();

		spot = null;
		minion = null;
	}

	public class MinionGone : StatusEffect
	{
		public override void OnRemove()
		{
			Destroy(target.gameObject);
		}
	}
}
