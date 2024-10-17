using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_StraightPath : Projectile
{
	public override void Travel()
	{
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
	}
}
