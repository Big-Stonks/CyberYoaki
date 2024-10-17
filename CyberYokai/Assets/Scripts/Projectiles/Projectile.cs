using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public EntityBehaviour target;

	public float speed;

	public bool targetReached { get => Vector3.Distance(transform.position, target.transform.position) < 0.1f; }

	public Action onTargetReached;

	private void Update()
	{
		Travel();

		if (targetReached)
		{
			onTargetReached?.Invoke();
			Destroy(gameObject);
		}
	}

	public virtual void Travel()
	{

	}
}
