using UnityEngine;
using System.Collections;

public class ReturnableSphericalDamageEvent : DamageEvent
{
	private AbstractReturnableProjectile projectile;
	private float radius;
	public ReturnableSphericalDamageEvent(int val, float radius, Vector3 loc, AbstractReturnableProjectile projectile) : base(val, loc) {
		this.radius = radius;
		this.projectile = projectile;
	}
	
	public override bool shouldPlayerHurt (Transform playerTransform)
	{
		return Vector3.Distance (playerTransform.position, this.location) <= this.radius;
	}

	public override void DamageReturn ()
	{
		projectile.ReturnProjectile ();
	}
}

