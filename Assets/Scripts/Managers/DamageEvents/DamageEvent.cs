using UnityEngine;
using System.Collections;

public abstract class DamageEvent
{
	protected int damageVal;
	protected Vector3 location;
	public Vector3 Location {
		get {
			return location;
		}
	}
	public int Value {
		get {
			return damageVal;
		}
	}

	public DamageEvent(int val, Vector3 loc) {
		this.damageVal = val;
		this.location = loc;
	}

	public abstract bool shouldPlayerHurt(Transform playerTransform);
	public virtual void DamageReturn() {}
}

