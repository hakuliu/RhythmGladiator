using UnityEngine;
using System.Collections;

public class BunnyProjectileMovement : AbstractProjectileScript, IHasSequentialStates
{
	public int attackDamage = 5;
	public float damageRadius = .6f;
	public float destroyFromTargetRadius = 1f;
	DamageManager damager;

	//game objects and scripts from outside
	GameObject player;
	Rigidbody body;
	LineRenderer gunLine;
	PlayerDelayedPosition poslookup;

	//events variables
	enum MovementState {Rising, Hovering, Shooting, Destroy};
	MovementState movementState;
	BeatTracker tracker;


	//some variables cached for movement behaviors
	float riseSpeed = .05f;
	Vector3 targetLoc;

	AudioSource audios;

	// Use this for initialization
	void Start ()
	{
		movementState = MovementState.Rising;
		player = GameObject.FindGameObjectWithTag ("Player");
		poslookup = player.GetComponent<PlayerDelayedPosition> ();
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
		gunLine = GetComponent <LineRenderer> ();
		this.audios = GetComponent<AudioSource> ();

		body = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		switch (movementState) {
		case MovementState.Rising:
			Rise ();
			break;
		case MovementState.Hovering:
			Hover();
			break;
		case MovementState.Shooting:
			Shoot();
			break;
		default:
			Destroy();
			break;
		}
	}
	void FixedUpdate ()
	{
		tracker.FixedUpdate ();
	}

	void Rise() 
	{
		Vector3 up = transform.TransformDirection(Vector3.forward);
		up = up.normalized;
		up *= riseSpeed;
		body.MovePosition(transform.position + up);
	}

	void Hover()
	{
		//TODO: oscillate or something?
	}

	void Shoot()
	{
		//decided to use lines instead
//		Vector3 movement = targetLoc - transform.position;
//		movement = movement.normalized * shootSpeed;
//		body.MovePosition(transform.position + movement);
//		if(Vector3.Distance(transform.position, targetLoc) <= destroyFromTargetRadius) {
//			Destroy();
//		}
	}

	void Destroy()
	{
		Destroy (gameObject);
	}
	/// <summary>
	/// assign to the track manager of this object that this projectile will:
	/// rise for 2 beats,
	/// hover for 2 beats,
	/// then shoot at the player (for one beat but it'll hit the ground by then)
	/// and then proceed to destroy itself
	/// </summary>
	/// <param name="tracker">Tracker.</param>
	void assignTrack(float[] deltas) 
	{

		BeatEvent[] events = new BeatEvent[]{
			CommonEventFactory.getNextStateEvent(this),
			CommonEventFactory.getNextStateEvent(this),
			CommonEventFactory.getNextStateEvent(this)
		};


		tracker.assignEvents (events, deltas);
	}
	public override void setTrackDelaysAndStart (float[] delays)
	{
		tracker = new BeatTracker ();
		assignTrack (delays);
		tracker.Start ();
	}
	
	void setTargetToPlayer()
	{
		//i think Vec3 in C# is a struct so this is safe and will copy, won't reference.
		targetLoc = poslookup.lookupPlayerPositionWithDelay ();
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		gunLine.SetPosition (1, targetLoc);
		damager.scheduleDamage(new SphericalDamageEvent(this.attackDamage, this.damageRadius, targetLoc));
	}

	void IHasSequentialStates.goToNextState() 
	{
		MovementState nextState = MovementState.Destroy;
		switch (movementState) {
		case MovementState.Rising:
			nextState = MovementState.Hovering;
			break;
		case MovementState.Hovering:
			setTargetToPlayer();
			audios.Play();
			nextState = MovementState.Shooting;
			break;
		case MovementState.Shooting:
			nextState = MovementState.Destroy;
			break;
		default:
			nextState = MovementState.Destroy;
			break;
		}
		this.movementState = nextState;
	}
}

