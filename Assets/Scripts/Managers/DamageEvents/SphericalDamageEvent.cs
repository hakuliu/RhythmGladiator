using UnityEngine;
using System.Collections;

public class SphericalDamageEvent : DamageEvent
{
	private float radius;
	public SphericalDamageEvent (int val, float radius, Vector3 loc) : base(val, loc) {
		this.radius = radius;
	}
	public override bool shouldPlayerHurt (Transform playerTransform)
	{
		return Vector3.Distance (playerTransform.position, this.location) <= this.radius;
	}
}

