using UnityEngine;
using System.Collections;

public class CannonHomingProjectile : AbstractProjectileScript, IHasSequentialStates
{
	float totalChaseTime;
	float currentTravelTime;
	
	protected override void Start ()
	{
		base.Start ();
		BeginChase ();
	}
	protected override void FixedUpdate ()
	{
		base.FixedUpdate ();
		ChaseUpdate ();
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
	}

	void ChaseUpdate() {
		if (this.currentTravelTime <= this.totalChaseTime) {
			Vector3 target = playerposlookup.lookupPlayerPositionWithDelay();
			Vector3 pos = this.transform.position;
			float timeleft = this.totalChaseTime - this.currentTravelTime;
			float deltatime = Time.fixedDeltaTime;
			Vector3 movement = (target - pos) / (timeleft / deltatime);
			movement.y = 0;
			this.transform.position += movement;
			this.currentTravelTime += deltatime;
		}
	}

	void IHasSequentialStates.goToNextState() {
	}
	
	void OnTriggerEnter(Collider col) {
		if(col == this.player.GetComponent<CapsuleCollider>()) {
		}
	}
}
