using UnityEngine;
using System.Collections;

public class KilledSetTriggeredEvent : AbstractEnemyManagerEvent
{
	//attempt to find this from hierarchy, so no need to set them now.
	private int enemyCount;
	public override void Start ()
	{
		base.Start ();
		enemyCount = this.eventCollection.Length;
	}
	public bool DecrementCount() {
		enemyCount--;
		return enemyCount == 0;
	}
	public override void doEvent ()
	{
		foreach (IEnemyInitiator ee in eventCollection) {
			ee.triggerStart();
		}
	}
}

