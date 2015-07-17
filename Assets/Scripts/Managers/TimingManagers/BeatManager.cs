using UnityEngine;
using System.Collections;
/// <summary>
/// Beat manager.
/// 
/// This class is designed to be the parent class for two main managers which will be
/// included in anything that fires off events that needs to be in sync with the music.
/// 
/// The parent class contains the static variable that identifies the level's beats per minute.
/// Since every level should only have one same beat per minute (unless we're dealing with some REALLY crazy music) this should be sufficient.
/// 
/// This class originally extended MonoBehavior, but with how I'm using it, it seemed like it's better to keep it as its own class.
/// </summary>
public abstract class BeatManager
{
	/// <summary>
	/// Sets the Beats per minute.
	/// Is actually stored in a private variable called tick,
	/// which denotes how long a quarter note takes in seconds
	/// </summary>
	/// <value>The beats per minute.</value>
	public static float BPM {
		get {
			float beat = 1f / tickTime;//beats per second
			beat *= 60f;//beats per minute
			return beat;
		}
		set {
			float ticktime = value / 60f;//beats per second
			ticktime = 1f / ticktime;//seconds per beat
			tickTime = ticktime;
		}
	}

	/// <summary>
	/// public access to time between each tick.  you can't edit it tho.
	/// </summary>
	/// <value>The tick time.</value>
	public static float TickTime {
		get {
			return tickTime;
		}
	}
	/// <summary>
	/// The time in seconds between each "tick"
	/// a tick is how long a quarter note is.
	/// </summary>
	protected  static float tickTime;
	
	/// <summary>
	/// Gets the delta time in seconds of a ratio of tick you want.
	/// for example, if you want the duration of time between two eith-notes, you'd pass 0.5f.
	/// </summary>
	/// <returns>time in seconds</returns>
	/// <param name="ofTick">the ratio of tick (quarter note) you want</param>
	public static float getDeltaTime(float ofTick) 
	{
		return tickTime * ofTick;
	}


	/// <summary>
	/// A generalized method to detect within FixedUpdate() if an event happened or not.
	/// for Tracks it means when an event happened, for Scheduler it's when the next beat happened.
	/// </summary>
	/// <returns><c>true</c>, if the event happened within the last update, <c>false</c> otherwise.</returns>
	/// <param name="lastTime">Last time of FixkedUpdate().</param>
	/// <param name="currentTime">Current time. (which should be lastTime + deltaTime</param>
	/// <param name="nextEventTime">Next event time.</param>
	public static bool eventHappened(float lastTime, float currentTime, float nextEventTime)
	{
		return (lastTime <= nextEventTime && nextEventTime < currentTime);
	}

	/// <summary>
	/// uses the current BPM set by BeatManager to find how many frames a given time interval in beat will be.
	/// Answer given in the FixedDeltaTime.
	/// </summary>
	/// <returns>The for beat.</returns>
	/// <param name="beat">Beat.</param>
	public static float FramesForBeat(float beat) {
		float totaltime = beat * tickTime;
		return totaltime / Time.fixedDeltaTime;
	}
}