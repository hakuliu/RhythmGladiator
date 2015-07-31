using UnityEngine;
using System.Collections;

public class LineLazer : AbstractEnemyAttack
{
	public float lazerEffectTime = .1f;
	public int damageVal = 10;
	public float[] deltas = new float[] {0f, 2f, 2f, 2f};
	public AudioClip[] shootingSounds;

	float range = 100f;
	LineRenderer liner;
	float effecttimer = 0;
	SoundRandomizer soundpicker;


	// Use this for initialization
	protected override void Start ()
	{
		base.Start ();
		liner = GetComponent<LineRenderer> ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
		soundpicker = new SoundRandomizer (audioSource, shootingSounds);
	}
	
	// Update is called once per frame
	protected override void FixedUpdate ()
	{
		base.FixedUpdate ();
		effecttimer += Time.fixedDeltaTime;
		if (effecttimer >= lazerEffectTime) {
			liner.enabled = false;
		}
	}

	protected override void assignTrack ()
	{
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
		damager.scheduleDamage (new LineDamageEvent (damageVal, this.transform.position, forward), 0f);
		soundpicker.PlayNext ();
	}

	DelegatedBeatEvent.DelegatedAction PewLambda() {
		return delegate() {
			Pew ();
		};
	}
}

