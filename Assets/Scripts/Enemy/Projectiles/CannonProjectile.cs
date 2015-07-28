using UnityEngine;
using System.Collections;

public class CannonProjectile : AbstractProjectileScript, IHasSequentialStates
{
	float totalChaseTime;
	float currentTravelTime;
	Vector3 velocity;

	protected override void Start ()
	{
		base.Start ();
		BeginChase ();
	}
	protected override void FixedUpdate ()
	{
		base.FixedUpdate ();
		TravelUpdate ();
	}

	public override void setTrackDelaysAndStart (float[] delays)
	{
		tracker = new BeatTracker ();
		assignTrack (delays);
		tracker.Start ();
	}
	void assignTrack(float[] delays) 
	{
		BeatEvent[] events = new BeatEvent[]{
			CommonEventFactory.getNextStateEvent(this),
		};
		float[] deltas = delays;
		
		this.tracker.assignEvents (events, deltas);
	}

	void BeginChase() {
		this.totalChaseTime = BeatManager.TimeForBeat (this.tracker.peekNextEventDelta ());
		this.currentTravelTime = 0f;

		float frames = totalChaseTime / Time.fixedDeltaTime;

		velocity = playerposlookup.lookupPlayerPositionWithDelay () - this.transform.position;
		velocity = velocity / frames;
	}
	void TravelUpdate() {
		this.transform.position = this.transform.position + velocity;
	}
	void IHasSequentialStates.goToNextState() {
	}
}

