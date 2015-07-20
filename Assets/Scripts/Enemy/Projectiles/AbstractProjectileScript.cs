using UnityEngine;
using System.Collections;

public abstract class AbstractProjectileScript : MonoBehaviour
{
	protected DamageManager damager;
	protected BeatTracker tracker;
	protected bool noTrackOverride;

	void Start() {
		noTrackOverride = false;
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
		ChildStart ();
	}

	void FixedUpdate() {
		if (tracker != null) {
			tracker.FixedUpdate ();
		} else {
			if(!noTrackOverride) {
				throw new UnityException("Pojectile " + this.ToString() + " has no track. and cannot do physics update.");
			}
		}
		ChildFixedUpdate ();
	}

	void Update() {
		ChildUpdate ();
	}

	public void setIgnoreMissingTrack(bool tf) {
		this.noTrackOverride = tf;
	}

	public abstract void setTrackDelaysAndStart(float [] delays);

	//i leave these non abstract and empty in case you don't want to actually override em.
	public virtual void ChildStart() {}
	public virtual void ChildFixedUpdate() {}
	public virtual void ChildUpdate() {}
}

