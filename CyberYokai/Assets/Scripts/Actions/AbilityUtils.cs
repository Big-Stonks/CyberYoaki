using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class for creating abilities.
/// </summary>
public class AbilityUtils : MonoBehaviour
{
	public static AbilityUtils instance;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	/// <summary>
	/// Class used to handle targeting by user
	/// </summary>
	public static class UserSelection
	{
		/// <summary>
		/// Get entity of type EntityBehaviour or any subclass of it when user clicks on an entity.
		/// </summary>
		/// <typeparam name="T">Which type of entity will be accepted.</typeparam>
		/// <returns>Returns clicked entity.</returns>
		public static T GetEntity<T>(out T target) where T : EntityBehaviour
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
			target = t;
			return t;
		}
	}

	/// <summary>
	/// Class used to handle entity selection by algorithm.
	/// </summary>
	public static class Selection
	{
		/// <summary>
		/// Get all entities in the same row as the target.
		/// </summary>
		/// <typeparam name="T">Which type of entity will be accepted.</typeparam>
		/// <param name="target">In row of which target?</param>
		/// <returns>Returns a list of type T with all the selected entitites.</returns>
		public static List<T> GetRowByTarget<T>(T target) where T : EntityBehaviour
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

		/// <summary>
		/// Get all entities in the same line as the target.
		/// </summary>
		/// <typeparam name="T">Which type of entity will be accepted. Is 'sideOnly' is false, this MUST be set as EntityBehaviour</typeparam>
		/// <param name="target">In line of which target?</param>
		/// <param name="sideOnly">Is only the entity's side checked or the whole line.</param>
		/// <returns>Returns a list of type T with all the selected entitites.</returns>
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
