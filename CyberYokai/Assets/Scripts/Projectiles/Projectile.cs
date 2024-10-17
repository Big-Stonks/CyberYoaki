using System;
using UnityEngine;

/// <summary>
/// Base class for handling projectile logic. It can be derived from in order to create unique trajectories.
/// </summary>
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
