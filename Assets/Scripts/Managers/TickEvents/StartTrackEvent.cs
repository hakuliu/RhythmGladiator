using UnityEngine;
using System.Collections;

public class StartTrackEvent : BeatEvent
{
	private BeatTracker track;
	public StartTrackEvent(BeatTracker track) 
	{
		this.track = track;
	}
	public override void doAction ()
	{
		track.Start ();
	}

}

