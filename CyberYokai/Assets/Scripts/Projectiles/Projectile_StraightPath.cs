using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that declares a straight path projectile. It will spawn on starting point and go straight to the end point by speed.
/// </summary>
public class Projectile_StraightPath : Projectile
{
	public override void Travel()
	{
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
	}
}
