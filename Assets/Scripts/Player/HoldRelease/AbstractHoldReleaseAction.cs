using UnityEngine;
using System.Collections;

public abstract class AbstractHoldReleaseAction
{
	//adjust to make engine feel better?
	//time measurements are all in beats rather than seconds.
	private static float preReleaseVariance = .2f;
	private static float postReleaseVariance = .3f;
	private static float holdBeginThreshold = .3f;

	public float releaseOptimalBeat = 2f;


	protected float pressedTime;
	protected bool pressed;

	public void Pressed() {
		pressed = true;
		pressedTime = 0;
		doInitialAction();
	}

	public void Released() {
		pressed = false;
		if (pressed && pressedTime >= holdBeginThreshold * BeatManager.TickTime) {
			float pre = (releaseOptimalBeat - preReleaseVariance) * BeatManager.TickTime;
			float post = (releaseOptimalBeat + postReleaseVariance) * BeatManager.TickTime;
			if(pressedTime >= pre && pressedTime <= post) {
				doReleasedAction();
			}
			//go on recoil
		}
	}

	public void FixedUpdateTimer() {
		if (pressed) {
			pressedTime += Time.fixedDeltaTime;
		}
	}

	public abstract void doInitialAction ();
	public abstract void doReleasedAction();
}

