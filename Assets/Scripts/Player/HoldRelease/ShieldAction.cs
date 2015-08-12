using UnityEngine;
using System.Collections;

public class ShieldAction : AbstractHoldReleaseAction
{
	private enum ShieldState {None, DReflect, Standard, OmniReflect};
	private ShieldState shieldState;
	private float shieldDecay = .3f;//seconds.
	private float shieldTime;
	private Transform playertransform;
	private float shieldDirectionAngle = 90f;
	public ShieldAction(Transform transform) : base(KeyCode.Mouse1) {
		shieldState = ShieldState.None;
		this.playertransform = transform;
	}
	protected override void doInitialAction ()
	{
		shieldState = ShieldState.DReflect;
		shieldTime = 0f;
	}
	protected override void doReleasedAction ()
	{
		shieldState = ShieldState.OmniReflect;
		shieldTime = 0f;
	}
	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		if (shieldState == ShieldState.DReflect) {
			if(shieldTime >= this.shieldDecay) {
				shieldState = ShieldState.Standard;
			}
		} else if(shieldState == ShieldState.Standard) {
			if(!pressed) {
				shieldState = ShieldState.None;
			}
		} else if(shieldState == ShieldState.OmniReflect) {
			if(shieldTime >= this.shieldDecay) {
				shieldState = ShieldState.None;
			}
		}

		shieldTime += Time.fixedDeltaTime;
	}
	public bool shouldTakeDamage(Vector3 projectileLoc) {
		if (shieldState == ShieldState.None) {
			return true;
		} else if (shieldState == ShieldState.OmniReflect) {
			return false;
		} else if (shieldState == ShieldState.DReflect || shieldState == ShieldState.Standard) {
			if(WithinShieldDirection(projectileLoc)) {
				return false;
			} else {
				return true;
			}

		} else {
			return true;
		}
	}
	public bool shouldReflect(Vector3 projectileLoc) {
		if (shieldState == ShieldState.OmniReflect) {
			return true;
		} else if (shieldState == ShieldState.DReflect && WithinShieldDirection (projectileLoc)) {
			return true;
		}
		return false;
	}
	public bool WithinShieldDirection(Vector3 projectileLoc) {
		Vector3 forward = playertransform.TransformDirection(Vector3.forward);
		Vector3 comp = projectileLoc - playertransform.position;
		float angle = Vector3.Angle(forward, comp);
		Debug.Log(angle);
		return angle < shieldDirectionAngle;
	}
}

