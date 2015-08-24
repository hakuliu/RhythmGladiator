using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private AbstractEnemyManagerEvent[] events;
	private int currentEventIndex;
	private static EnemyManager self;
    void Start ()
    {
		events = GetComponentsInChildren<AbstractEnemyManagerEvent> ();
		Debug.Log (events.Length);
		currentEventIndex = 0;
		TriggerCurrentEvent ();
    }

	public void countDownEnemy() 
	{
		AbstractEnemyManagerEvent current = GetCurrentEventTrigger ();
		if (current != null) {
			if(current is KilledSetTriggeredEvent) {
				KilledSetTriggeredEvent casted = current as KilledSetTriggeredEvent;
				if(casted.DecrementCount()) {
					AdvanceNextEvent();
				}
			}
		}
	}
	private void AdvanceNextEvent() {
		currentEventIndex++;
		if (currentEventIndex >= events.Length) {
			//we're done here
			LoadNextLevel ();
		} else {
			TriggerCurrentEvent();
		}
	}
	private void TriggerCurrentEvent() {
		AbstractEnemyManagerEvent totrigger = GetCurrentEventTrigger();
		if(totrigger != null) {
			totrigger.doEvent();
		}
	}
	private void LoadNextLevel() {
		int i = Application.loadedLevel;
		Application.LoadLevel(i+1);
	}
	private AbstractEnemyManagerEvent GetCurrentEventTrigger() {
		if (currentEventIndex >= 0 && currentEventIndex < this.events.Length) {
			return this.events [this.currentEventIndex];
		} else {
			return null;
		}
	}
	public static EnemyManager Get() {
		if (self == null) {
			GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
			self = managers.GetComponentInChildren<EnemyManager> ();
		}
		return self;
	}
}
