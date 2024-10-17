using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpot : MonoBehaviour
{
	public enum SpotSide { Player, Enemy }

	public Vector2Int coords;
	public EntityBehaviour entityOnThis;
	public SpotSide side;
}
