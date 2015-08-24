using UnityEngine;
using System.Collections;

public class EnemyStartEvent : MonoBehaviour, IEnemyInitiator
{
	public GameObject enemy;
	private AbstractEnemyAttack attackscript;
	// Use this for initialization
	void Start ()
	{
		attackscript = enemy.GetComponent<AbstractEnemyAttack> ();
	}

	void IEnemyInitiator.triggerStart() {
		BeatTracker track = attackscript.GetTrack ();
		if (track != null) {
			BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
		}
	}
}

