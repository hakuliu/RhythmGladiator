using UnityEngine;
using System.Collections;

public class GroundSpikeProjectile : AbstractProjectileScript, IHasSequentialStates
{
	public float decayTimeS = 1f;
	public float debrisDistanceInterval = 5f;
	public GameObject debrisObject;
	float totalChaseTime;
	float currentTravelTime;
	float decayTimer = 0;
	bool decaying = false;
	Vector3 lastDebrisPos;

	// Use this for initialization
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
	protected override void Update ()
	{
		base.Update ();
		DecayUpdate ();
		DebrisUpdate ();
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



	void IHasSequentialStates.goToNextState() {
		//next state is definitely to schedule damage.
		decaying = true;
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
			this.transform.position += movement;
			this.currentTravelTime += deltatime;
		}
	}
	void DecayUpdate() {
		if(decaying) {
			if (decayTimer >= this.decayTimeS) {
				Destroy (this.gameObject);
			}
			decayTimer += Time.deltaTime;
		}
	}
	void DebrisUpdate() {
		if (this.currentTravelTime <= this.totalChaseTime) {
			Vector3 pos = this.transform.position;
			if(Vector3.Distance(lastDebrisPos, pos) >= this.debrisDistanceInterval) {
				lastDebrisPos = pos;
				Instantiate(debrisObject, this.transform.position, this.transform.rotation);
			}
		}
	}
}

