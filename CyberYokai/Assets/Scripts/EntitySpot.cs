using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used on a game object to declare it as a spot.
/// Holds the data for rows, lines, sides, and so on.
/// </summary>
public class EntitySpot : MonoBehaviour
{
	public enum SpotSide { Player, Enemy }

	public Vector2Int coords;
	public EntityBehaviour entityOnThis;
	public SpotSide side;
}
