using UnityEngine;
using System.Collections;

public class BeatTracker : BeatManager
{
	/// <summary>
	/// primary variable used to do timing events. gets updated every FixedUpdate.
	/// </summary>
	private float timer;
	
	private BeatEvent[] events;//the next event.
	private float[] deltas;//as with MIDI files, this is in 'tick's for the next event
	private bool loop;//loop or not
	private int nextIndex;//the awaiting event
	private float lastEventTime = 0;

	private bool started = false;


	/// <summary>
	/// Assigns the events. TODO: copy the events and deltas passed in, otherwise these are still pointers
	/// and can change unexpectedly... (so don't change them plz)
	/// </summary>
	/// <param name="events">Events.</param>
	/// <param name="deltas">Deltas.</param>
	public void assignEvents(BeatEvent[] events, float[] deltas) 
	{
		this.events = events;
		this.deltas = deltas;
	}

	// Use this for initialization
	public void Start ()
	{
		started = true;
		timer = 0;
		lastEventTime = 0;
		loop = true;
	}

	// I use FixedUpdate here because the timing of the beat should be based on real time,
	// and should not lag when the game is lagging, or it'll make a pretty awful experience.
	public void FixedUpdate ()
	{
		if (this.events == null) {
			Debug.Log("There are no events in this track.");
			return;
		}
		if (started) {
			float lastTime = timer;
			timer += Time.fixedDeltaTime;
			
			if (nextIndex >= 0 && nextIndex < this.events.Length) {
				float nextEventTime = lastEventTime + this.deltas[nextIndex] * tickTime;
				if(eventHappened(lastTime, timer, nextEventTime)) {
					//do the event
					this.events[nextIndex].doAction();
					lastEventTime = nextEventTime;
					nextIndex++;
					if(loop && this.nextIndex >= this.events.Length) {
						nextIndex = 0;
						//if we're looping we gotta do the event right away.
						//normally the last thing in event track is a no-op
						this.events[nextIndex].doAction();
						nextIndex++;
					}
				}
			}
		}
	}

	/// <summary>
	/// Inserts the offset to start of the track.
	/// should be called before track has been started. if it has already started, we return false and don't do anything.
	/// also fails and wont' do anything if there was no track assigned already.
	/// </summary>
	/// <returns><c>true</c>, if offset was inserted, <c>false</c> otherwise.</returns>
	/// <param name="beatvalue">Beatvalue.</param>
	public bool AdditionalOffset(float beatvalue) {
		if (!started && events != null && deltas != null) {
			deltas[0] += beatvalue;
		}
		return false;
	}

}

