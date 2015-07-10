using UnityEngine;
using System.Collections;

public class SpawnEvent : BeatEvent
{
	private GameObject spawnEntity;
	private Transform spawnPoint;
	public delegate void AfterSpawnAction();
	AfterSpawnAction afterAction = () =>{};
	public SpawnEvent(GameObject entity, Transform point)
	{
		this.spawnEntity = entity;
		this.spawnPoint = point;
	}
	public void setAfterSpawnAction(AfterSpawnAction action) {
		afterAction = action;
	}
	public override void doAction ()
	{
		Debug.Log ("doing spawn event");
		MonoBehaviour.Instantiate (spawnEntity, spawnPoint.position, spawnPoint.rotation);
		afterAction ();
	}

}

