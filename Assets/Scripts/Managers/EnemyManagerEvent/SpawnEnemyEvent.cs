using UnityEngine;
using System.Collections;

public class SpawnEnemyEvent : MonoBehaviour, IEnemyInitiator
{
	public Transform spawnLocation;
	public GameObject spawnPrefab;
	public bool startTrack = true;
	void IEnemyInitiator.triggerStart() {
		GameObject inst = GameObject.Instantiate (spawnPrefab, spawnLocation.position, spawnLocation.rotation) as GameObject;
		if (startTrack) {
			AbstractEnemyAttack attackscript = inst.GetComponent<AbstractEnemyAttack> ();
			if (attackscript != null) {
				BeatTracker track = attackscript.GetTrack ();
				if (track != null) {
					BeatScheduler.ScheduleNextMeasure (new StartTrackEvent (track));
				}
			}
		}


	}
}

