using UnityEngine;
using System.Collections;

public class ShieldAction : AbstractHoldReleaseAction
{
	private enum ShieldState {None, DReflect, Standard, OmniReflect};
	private ShieldState shieldState;
	private float shieldDecay = .3f;//seconds.
	private float shieldTime;
	public ShieldAction() : base(KeyCode.Mouse1) {
		shieldState = ShieldState.None;
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
		return shieldState == ShieldState.None;
	}
	public bool shouldReflect(Vector3 projectileLoc) {
		return (shieldState == ShieldState.DReflect || shieldState == ShieldState.OmniReflect);
	}
}

