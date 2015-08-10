using UnityEngine;
using System.Collections;

public abstract class AbstractHoldReleaseAction
{
	//adjust to make engine feel better?
	//time measurements are all in beats rather than seconds.
	protected static float preReleaseVariance = .2f;
	protected static float postReleaseVariance = .3f;
	protected static float holdBeginThreshold = .3f;

	public float releaseOptimalBeat = 2f;


	protected float pressedTime;
	private bool lastPressed;
	protected bool pressed;
	private KeyCode key;
	private bool held = false;

	public AbstractHoldReleaseAction(KeyCode key) {
		this.key = key;
	}

	public virtual void Pressed() {
		pressed = true;
		pressedTime = 0;
		doInitialAction();
	}

	public virtual void Released() {
		held = false;
		if (pressedTime >= holdBeginThreshold * BeatManager.TickTime) {

			float pre = (releaseOptimalBeat - preReleaseVariance) * BeatManager.TickTime;
			float post = (releaseOptimalBeat + postReleaseVariance) * BeatManager.TickTime;
			if(pressedTime >= pre && pressedTime <= post) {
				doReleasedAction();
			}
			removeHoldPhysicsBehavior();
			EndHoldEffect();
			applyRecoil ();
		}
		pressed = false;
	}

	public virtual void FixedUpdate() {
		pressed = Input.GetKey (key);
		if (pressed) {
			if(pressedTime >= holdBeginThreshold * BeatManager.TickTime && !held) {
				held = true;
				applyHoldPhysicsBehavior();
				BeginHoldEffect();
			}
			pressedTime += Time.fixedDeltaTime;
		}
		if (PressedLastFrame()) {
			Pressed();
		}
		if (ReleasedLastFrame()) {
			Released();
		}
		lastPressed = pressed;
	}
	public virtual void Update() {
		if (pressed) {
			HoldEffectUpdate ();
		}
	}
	/// <summary>
	/// for example, slow down player movement while holding.
	/// </summary>
	protected virtual void applyHoldPhysicsBehavior(){}
	protected virtual void removeHoldPhysicsBehavior() {}

	protected virtual void applyRecoil() {}

	protected virtual void BeginHoldEffect() {}
	protected virtual void HoldEffectUpdate() {}
	protected virtual void EndHoldEffect() {}

	protected abstract void doInitialAction ();
	protected abstract void doReleasedAction();

	private bool PressedLastFrame() {
		return !this.lastPressed && this.pressed;
	}
	private bool ReleasedLastFrame() {
		return this.lastPressed && !this.pressed;
	}
}

