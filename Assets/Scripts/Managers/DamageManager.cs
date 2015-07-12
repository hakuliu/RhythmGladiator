using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{
	public float delayTime = .3f;//seconds
	public PlayerHealth playerHealthScript;
	public Transform playerTransform;
	private ArrayList scheduledDamages;

	// Use this for initialization
	void Start ()
	{
		scheduledDamages = new ArrayList();
	}

	void FixedUpdate() {
		float currenttime = Time.fixedTime;
		updateScheduledEvents (currenttime);
		//flushExpiredEvents (currenttime);
	}

	void updateScheduledEvents(float t) {
		//make a copy first, because the original arraylist will likely change.
		ArrayList copy = new ArrayList (scheduledDamages);
		foreach (DamageScheduledEvent e in copy) {
			if(t - e.scheduledTime > this.delayTime) {
				runEvent(e.scheduledEvent);
				scheduledDamages.Remove(e);
			}
		}
	}

	void runEvent(DamageEvent e) {
		if (e.shouldPlayerHurt (playerTransform)) {
			playerHealthScript.HandleDamageEvent(e);
		}
	}

	public void scheduleDamage(DamageEvent e) {
		DamageScheduledEvent s = new DamageScheduledEvent (Time.fixedTime, e);
		scheduledDamages.Add (s);
	}
	private class DamageScheduledEvent {
		
		public float scheduledTime;
		public DamageEvent scheduledEvent;
		public DamageScheduledEvent(float t, DamageEvent e) {
			this.scheduledTime = t;
			this.scheduledEvent = e;
		}
		public override bool Equals (object obj)
		{
			if (obj is DamageScheduledEvent) {
				DamageScheduledEvent other = (DamageScheduledEvent)obj;
				if(this.scheduledTime == other.scheduledTime) {
					//pointer equality is fine here, i think.
					if(this.scheduledEvent == other.scheduledEvent) {
						return true;
					}
				}
			}
			return false;
		}
		//meh don't rly need this since i'm not putting it in hash?
		//unless ArrayList does...
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}

