using UnityEngine;
using System.Collections;

public class SpawnEnemyEvent : MonoBehaviour, IEnemyInitiator
{
	public Transform spawnLocation;
	public GameObject spawnPrefab;
	public bool startTrack = true;
	void IEnemyInitiator.triggerStart() {

	}
}

