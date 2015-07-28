using UnityEngine;
using System.Collections;

public abstract class AbstractSingleAttack : AbstractEnemyAttack
{
	public GameObject projectile;
	public float beatOffset;
	public float beatsBetweenAttack = 8f;


	protected override void assignTrack() {
		BeatEvent[] events = new BeatEvent[]{
			getAttackEvent (),
			CommonEventFactory.getNoOp ()
		};
		float[] deltas = new float[]{
			beatOffset,
			beatsBetweenAttack
		};
		
		track.assignEvents (events, deltas);
		track.AdditionalOffset (beatOffset);
	}

	BeatEvent getAttackEvent()
	{
		return CommonEventFactory.getDelegatedEvent (delegate() {
			Attack ();
		});
	}

	protected abstract void Attack();
}

