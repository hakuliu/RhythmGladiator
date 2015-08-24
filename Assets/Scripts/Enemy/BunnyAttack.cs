using UnityEngine;
using System.Collections;

public class BunnyAttack : AbstractEnemyAttack
{
	public GameObject projectile;
	public AudioClip fireSound;
	public float[] deltas = new float[]{0f, 2f, 2f, 12f};
	public float[] projectileDeltas = new float[]{4f, 4f, .5f};

    PlayerHealth playerHealth;
	int attackCounter;//used to determine which rotation to fire at.


    protected override void Start ()
	{
		base.Start ();
        playerHealth = player.GetComponent <PlayerHealth> ();
    }

	protected override void assignTrack ()
	{
		BeatEvent[] events = new BeatEvent[deltas.Length];
		for(int i = 0 ; i < events.Length - 1; i++)
		{
			events[i] = getAttackEvent ();
		}
		events [events.Length - 1] = CommonEventFactory.getNoOp ();

		track.assignEvents (events, deltas);
	}

	BeatEvent getAttackEvent()
	{
		return CommonEventFactory.getDelegatedEvent (delegate() {
			Attack ();
		});
	}

    void Attack ()
    {
        if(playerHealth.currentHealth > 0)
        {

			audioSource.PlayOneShot(fireSound, .5f);
			Vector3 upabit = transform.TransformDirection(Vector3.up);

			Vector3 rothelper = upabit;
			if(attackCounter % deltas.Length == 0) {
				rothelper += transform.TransformDirection(Vector3.right)/3f;
			} else if(attackCounter % 3 == 1) {
				//do nothing, directly up
			} else {
				rothelper += transform.TransformDirection(Vector3.left)/3f;
			}
			Quaternion pRot = Quaternion.LookRotation(rothelper);
			GameObject pro;
			pro = Instantiate(projectile, this.transform.position + upabit, pRot) as GameObject;
			AbstractProjectileScript proscript = pro.GetComponent<AbstractProjectileScript>();
			proscript.setTrackDelaysAndStart(projectileDeltas);
			anim.SetTrigger("Fire");
			attackCounter++;
        }
    }
}
