using UnityEngine;
using System.Collections;

public class GroundDamageEvent : DamageEvent
{
	/// <summary>
	/// radius from source. if it's 0 or negative, we assume you want the whole ground to take damage.
	/// </summary>
	private float radius;
	private static float groundDamageThreshold = .3f;//adjust in code
	public GroundDamageEvent(int val, float radius, Vector3 loc) : base(val, loc) {
		this.radius = radius;
	}
	public GroundDamageEvent(int val, Vector3 loc) : base(val, loc) {
		this.radius = 0;
	}
	public override bool shouldPlayerHurt (Transform playerTransform)
	{
		Vector3 ongroundpos = playerTransform.position;
		//we assume ground is at y = 0.
		ongroundpos.y = 0;
		Debug.Log(Vector3.Distance (ongroundpos, this.location));
		if (radius <= 0 || Vector3.Distance (ongroundpos, this.location) <= radius) {
			return playerTransform.position.y <= groundDamageThreshold;
		}
		return false;
	}
}

