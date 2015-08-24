using UnityEngine;
using System.Collections;

public class CollectionEnemyManagerEvent : AbstractEnemyManagerEvent
{
	/// <summary>
	/// The event collection.
	/// if this is left empty in the unity editor, we'll just go ahead and look for them within the hierarchy.
	/// </summary>
	public AbstractEnemyManagerEvent[] eventCollection;
	void Start() {
		if(eventCollection == null || eventCollection.Length == 0) {
			AbstractEnemyManagerEvent[] temp = this.GetComponentsInChildren<AbstractEnemyManagerEvent>();
			this.eventCollection = temp;
		}
	}

	public override void doEvent ()
	{
		foreach (AbstractEnemyManagerEvent ee in this.eventCollection) {
			ee.doEvent();
		}
	}
}

