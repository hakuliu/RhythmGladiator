using UnityEngine;
using System.Collections;

public abstract class AbstractProjectileScript : MonoBehaviour
{
	protected DamageManager damager;
	protected BeatTracker tracker;
	protected bool noTrackOverride;
	protected PlayerDelayedPosition playerposlookup;
	protected GameObject player;


	protected virtual void Start() {
		noTrackOverride = false;
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
		
		player = GameObject.FindGameObjectWithTag ("Player");
		playerposlookup = player.GetComponent<PlayerDelayedPosition> ();
	}

	protected virtual void FixedUpdate() {
		if (tracker != null) {
			tracker.FixedUpdate ();
		} else {
			if(!noTrackOverride) {
				throw new UnityException("Pojectile " + this.ToString() + " has no track. and cannot do physics update.");
			}
		}
	}

	protected virtual void Update() {
	}

	public void setIgnoreMissingTrack(bool tf) {
		this.noTrackOverride = tf;
	}

	public abstract void setTrackDelaysAndStart(float [] delays);

}

