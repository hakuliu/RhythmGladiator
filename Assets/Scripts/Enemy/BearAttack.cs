using UnityEngine;
using System.Collections;

public class BearAttack : MonoBehaviour
{
	public GameObject bomb;
	public float beatOffset;
	public float beatsBetweenAttack = 8f;
	public float beatsTilDetonation = 4f;

	BeatTracker track;
	AudioSource audioSource;
	Animator anim;
	GameObject projectile;

	// Use this for initialization
	void Start ()
	{
		track = new BeatTracker ();
		assignTrack ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
		audioSource = GetComponent <AudioSource> ();
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		track.FixedUpdate ();
	}

	void assignTrack() {
		BeatEvent[] events = new BeatEvent[]{
			getAttackEvent (),
			CommonEventFactory.getNoOp ()
		};
		float[] deltas = new float[]{
			0f,
			8f
		};
		
		track.assignEvents (events, deltas);
		track.AdditionalOffset (beatOffset);
	}

	BeatEvent getAttackEvent()
	{
		return CommonEventFactory.getDelegatedEvent (delegate() {
			LayBomb();
		});
	}

	void LayBomb()
	{
		audioSource.Play ();
		Vector3 upabit = Vector3.up;
		this.transform.TransformDirection (upabit);
		upabit *= 1.3f;

		projectile = Instantiate(bomb, this.transform.position + upabit, this.transform.rotation) as GameObject;
		AbstractProjectileScript b = projectile.GetComponent<AbstractProjectileScript> ();
		b.setTrackDelaysAndStart (new float[]{beatsTilDetonation});
		anim.SetTrigger("Fire");
	}
}

