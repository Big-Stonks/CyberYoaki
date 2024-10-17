using System.Collections.Generic;
using UnityEngine;

public class AbilityUtils : MonoBehaviour
{
	public static AbilityUtils instance;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void DealDamage(Ability ability)
	{
		Debug.Log("Hit with " + ability.name);
	}

	/// <summary>
	/// Class used to handle targeting by user
	/// </summary>
	public static class UserSelection
	{
		public static T GetEntity<T>() where T : EntityBehaviour
		{
			T t = null;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 1000f, LayerMask.GetMask("Entity")))
			{
				if (hit.collider.GetComponent<T>() != null)
				{
					if (Input.GetMouseButtonDown(0))
					{
						t = hit.collider.GetComponent<T>();
					}
				}
			}
			return t;
		}
	}

	/// <summary>
	/// Class used to handle entity selection by algorithm.
	/// </summary>
	public static class Selection
	{
		public static List<T> GetRowByTarget<T>(EntityBehaviour target) where T : EntityBehaviour
		{
			List<EntitySpot> spots = CombatManager.instance.spots.FindAll(es => es.coords.x == target.spot.coords.x);

			List<T> result = new List<T>();
			foreach (EntitySpot spot in spots)
			{
				if (spot.entityOnThis != null)
					result.Add(spot.entityOnThis as T);
			}

			return result;
		}

		public static List<T> GetLineByTarget<T>(EntityBehaviour target, bool sideOnly = true) where T : EntityBehaviour
		{
			List<EntitySpot> spots = CombatManager.instance.spots.FindAll(es => es.coords.y == target.spot.coords.y);

			if (sideOnly)
				spots.RemoveAll(es => es.side != target.spot.side);

			List<T> result = new List<T>();
			foreach (EntitySpot spot in spots)
			{
				if (spot.entityOnThis != null)
					result.Add(spot.entityOnThis as T);
			}

			return result;
		}
	}

}
