using UnityEngine;
using System.Collections;

public class BunnyProjectileMovement : MonoBehaviour, IHasSequentialStates
{
	public int attackDamage = 5;
	public float damagedelay = .2f;
	public float damageRadius = .6f;
	public float destroyFromTargetRadius = 1f;

	//game objects and scripts from outside
	GameObject player;
	GameObject floor;
	PlayerHealth playerHealth;
	Rigidbody body;
	LineRenderer gunLine;

	//events variables
	enum MovementState {Rising, Hovering, Shooting, Destroy};
	MovementState movementState;
	BeatTracker tracker;
	bool launched = false;
	float launchtimer = 0;


	//some variables cached for movement behaviors
	float riseSpeed = .05f;
	Vector3 targetLoc;

	AudioSource audios;

	// Use this for initialization
	void Start ()
	{
		movementState = MovementState.Rising;
		player = GameObject.FindGameObjectWithTag ("Player");
		floor = GameObject.FindGameObjectWithTag ("Floor");
		playerHealth = player.GetComponent <PlayerHealth> ();
		gunLine = GetComponent <LineRenderer> ();
		this.audios = GetComponent<AudioSource> ();
		tracker = new BeatTracker ();
		assignTrack (tracker);
		tracker.Start ();


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
		updateDamageApplicator ();
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
	void assignTrack(BeatTracker tracker) 
	{
		BeatEvent[] events = new BeatEvent[]{
			CommonEventFactory.getNextStateEvent(this),
			CommonEventFactory.getNextStateEvent(this),
			CommonEventFactory.getNextStateEvent(this)
		};
		float[] deltas = new float[]{
			4f,
			4f,
			2f
		};

		this.tracker.assignEvents (events, deltas);
	}
	

	void ApplyDamage ()
	{	
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
	void updateDamageApplicator()
	{
		if (launched) {
			launchtimer += Time.fixedDeltaTime;
			if(launchtimer >= damagedelay) {
				checkAndAttack();
			}
		}
	}
	void checkAndAttack()
	{
		if (Vector3.Distance (player.transform.position, targetLoc) <= damageRadius) {
			ApplyDamage ();
		}
		Destroy ();
	}
	void setTargetToPlayer()
	{
		//i think Vec3 in C# is a struct so this is safe and will copy, won't reference.
		targetLoc = player.transform.position;
		launched = true;
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		gunLine.SetPosition (1, targetLoc);
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

