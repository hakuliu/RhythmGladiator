using UnityEngine;
using System.Collections;

public class GroundBombAttack : MonoBehaviour, IHasSequentialStates
{
	public float closeEnoughToGround = .3f;
	public int damage = 15;
	public float damagedelay = .2f;//to account for human reflex and jumping delay
	BeatTracker tracker;
	Rigidbody body;
	Transform playertransform;
	PlayerHealth playerHealth;
	float delaytimer = 0f;
	bool detonating = false;
	MeshRenderer meshrenderer;
	Light redlight;
	AudioSource audios;

	// Use this for initialization
	void Start ()
	{
		tracker = new BeatTracker ();
		assignTrack ();
		tracker.Start ();
		body = GetComponent<Rigidbody> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playertransform = player.transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		this.meshrenderer = GetComponent<MeshRenderer> ();
		redlight = GetComponent<Light> ();
		this.audios = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		tracker.FixedUpdate ();
		updateSeeIfAttack ();
	}

	void assignTrack() 
	{
		BeatEvent[] events = new BeatEvent[]{
			CommonEventFactory.getNextStateEvent(this),
		};
		float[] deltas = new float[]{
			4f
		};
		
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
		redlight.range = 20f;
		detonating = true;
		delaytimer = 0;
	}

	void updateSeeIfAttack() {
		if (detonating) {
			delaytimer += Time.fixedDeltaTime;
			if(delaytimer >= damagedelay) {
				if (IsPlayerOnGround ()) {
					Attack();
				}
				//finally destroy this object
				Destroy (gameObject);
			}
		}
	}

	void Attack() {
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (damage);
		}
	}

	/// <summary>
	/// yeah yeah, I could use Physics.Raycast and check how far the ground is...but we know the floor is at y=0...
	/// why bother doing such intense calculations?
	/// </summary>
	/// <returns><c>true</c>, if player on ground, <c>false</c> otherwise.</returns>
	bool IsPlayerOnGround() 
	{
		return playertransform.position.y < closeEnoughToGround;
	}
}

