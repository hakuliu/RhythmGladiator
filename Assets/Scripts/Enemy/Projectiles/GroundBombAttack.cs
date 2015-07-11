using UnityEngine;
using System.Collections;

public class GroundBombAttack : MonoBehaviour, IHasSequentialStates
{
	public float closeEnoughToGround = .3f;
	public int damage = 15;
	DamageManager damager;
	BeatTracker tracker;
	Rigidbody body;
	Transform playertransform;
	PlayerHealth playerHealth;
	MeshRenderer meshrenderer;
	Light redlight;
	AudioSource audios;
	Vector3 bombpos;

	// Use this for initialization
	void Start ()
	{
		tracker = new BeatTracker ();
		assignTrack ();
		tracker.Start ();
		body = GetComponent<Rigidbody> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
		playertransform = player.transform;
		playerHealth = player.GetComponent <PlayerHealth> ();
		this.meshrenderer = GetComponent<MeshRenderer> ();
		redlight = GetComponent<Light> ();
		this.audios = GetComponent<AudioSource> ();
		this.bombpos = GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		tracker.FixedUpdate ();
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
		damager.scheduleDamage(new GroundDamageEvent(this.damage, bombpos));
    }
}

