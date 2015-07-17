using UnityEngine;
using System.Collections;

public class LineLazer : MonoBehaviour
{
	public float lazerEffectTime = .1f;
	public int damageVal = 10;
	public float[] deltas = new float[] {0f, 2f, 2f, 2f};

	float range = 100f;
	LineRenderer liner;
	BeatTracker track;
	GameObject player;
	float effecttimer = 0;
	DamageManager damager;


	// Use this for initialization
	void Start ()
	{
		liner = GetComponent<LineRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		track = new BeatTracker ();
		PopulateTrack ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		track.FixedUpdate ();
		effecttimer += Time.fixedDeltaTime;
		if (effecttimer >= lazerEffectTime) {
			liner.enabled = false;
		}
	}

	void PopulateTrack() {
		BeatEvent[] events = new BeatEvent[deltas.Length];
		for (int i = 0; i < events.Length; i++) {
			events[i] = CommonEventFactory.getDelegatedEvent (PewLambda ());
		}
		events [events.Length - 1] = CommonEventFactory.getNoOp ();
		track.assignEvents (events, deltas);
	}

	void Pew() {
		//audioSource.PlayOneShot(fireSound, .5f);
		effecttimer = 0;
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		forward *= range;
		forward += transform.position;
		liner.enabled = true;
		liner.SetPosition (0, transform.position);
		liner.SetPosition (1, forward);
		damager.scheduleDamage (new LineDamageEvent (damageVal, this.transform.position, forward));
	}

	DelegatedBeatEvent.DelegatedAction PewLambda() {
		return delegate() {
			Pew ();
		};
	}
}

