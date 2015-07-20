using UnityEngine;
using System.Collections;

public class GroundBombAttack : AbstractProjectileScript, IHasSequentialStates
{
	public int damage = 15;
	public float explosiontime = .5f;
	public float startingRadius = 5f;
	public float explodeRadius = 6f;

	MeshRenderer meshrenderer;
	Light redlight;
	AudioSource audios;
	Vector3 bombpos;
	float exploded = -1f;

	// Use this for initialization
	protected override void Start()
	{
		base.Start ();
		this.meshrenderer = GetComponent<MeshRenderer> ();
		redlight = GetComponent<Light> ();
		redlight.range = startingRadius;
		this.audios = GetComponent<AudioSource> ();
		this.bombpos = GetComponent<Transform> ().position;
	}

	public override void setTrackDelaysAndStart(float [] delays) {
		tracker = new BeatTracker ();
		assignTrack (delays);
		tracker.Start ();
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update ();
		//this doesn't really need to be that accurate.
		UpdateExplosion ();
	}

	void assignTrack(float[] delays) 
	{
		BeatEvent[] events = new BeatEvent[]{
			CommonEventFactory.getNextStateEvent(this),
		};
		float[] deltas = delays;
		
		this.tracker.assignEvents (events, deltas);
	}

	void doBounceAnim()
	{

	}

	/// <summary>
	/// There's only two states, so "next" state just triggers the bombing.
	/// </summary>
	void IHasSequentialStates.goToNextState() 
	{
		ScheduleAttack ();
	}
	void ScheduleAttack() {
		this.meshrenderer.enabled = false;
		audios.Play ();
		redlight.range = 20;
		damager.scheduleDamage(new GroundDamageEvent(this.damage, explodeRadius, bombpos));
		exploded = 0;
    }
	void UpdateExplosion() {
		if (exploded != -1) {
			exploded += Time.deltaTime;
			if(exploded >= this.explosiontime) {
				Destroy (gameObject);
			}
		}
	}
}

