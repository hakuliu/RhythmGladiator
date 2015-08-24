using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyManagerEvent : MonoBehaviour
{
	protected IEnemyInitiator[] eventCollection;
	public virtual void Start() {
		if(eventCollection == null || eventCollection.Length == 0) {
			IEnemyInitiator[] temp = this.GetComponentsInChildren<IEnemyInitiator>();
			this.eventCollection = temp;
		}
	}
	public abstract void doEvent();
}

