using UnityEngine;
using System.Collections;

public class LineDamageEvent : DamageEvent
{
	int mask;
	Vector3 end;
	public LineDamageEvent(int val, Vector3 startPos, Vector3 endPos) : base(val, startPos) {
		end = endPos;
		mask = LayerMask.GetMask ("Player");
	}

	public override bool shouldPlayerHurt (Transform playerTransform)
	{
		Vector3 dir = this.end - this.location;
		Ray ray = new Ray (this.location, dir.normalized);

		return Physics.Raycast (ray, Vector3.Distance (this.location, this.end), this.mask);
	}
}

