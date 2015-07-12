using UnityEngine;
using System.Collections;

public class BunnyAttack : MonoBehaviour
{
	public GameObject projectile;
	public AudioClip fireSound;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
	BeatTracker track;
	int attackCounter;//used to determine which rotation to fire at.
	AudioSource audioSource;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
		audioSource = GetComponent <AudioSource> ();
        anim = GetComponent <Animator> ();
		track = new BeatTracker ();
		assignTrack ();
		BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
    }

	void assignTrack() {
		BeatEvent[] events = new BeatEvent[]{
			getAttackEvent (),
			getAttackEvent (),
			getAttackEvent (),
			CommonEventFactory.getNoOp ()
		};
		float[] deltas = new float[]{
			0f,//first offset actually gets ignored at loop, so that's why we got 5-beat noop still
			2f,
			2f,
			12f
		};

		track.assignEvents (events, deltas);
	}

	BeatEvent getAttackEvent()
	{
		return CommonEventFactory.getDelegatedEvent (delegate() {
			Attack ();
		});
	}



    void FixedUpdate ()
    {
		track.FixedUpdate ();
    }


    void Attack ()
    {
        if(playerHealth.currentHealth > 0)
        {

			audioSource.PlayOneShot(fireSound, .5f);
			Vector3 upabit = transform.TransformDirection(Vector3.up);

			Vector3 rothelper = upabit;
			if(attackCounter % 3 == 0) {
				rothelper += transform.TransformDirection(Vector3.right)/3f;
			} else if(attackCounter % 3 == 1) {
				//do nothing, directly up
			} else {
				rothelper += transform.TransformDirection(Vector3.left)/3f;
			}
			Quaternion pRot = Quaternion.LookRotation(rothelper);

			Instantiate(projectile, this.transform.position + upabit, pRot);
			anim.SetTrigger("Fire");
			attackCounter++;
        }
    }
}
