using UnityEngine;
using System.Collections;

/// <summary>
/// A Beat event that takes a delegate so that you can anonymously asign actions
/// this is the most "powerful" way of implementing as it allows the event to do really anything
/// however it's also the most unsafe and most "spaghetti code" of the implementations,
/// so avoid using this if possible.
/// </summary>
public class DelegatedBeatEvent : BeatEvent
{
	public delegate void DelegatedAction();
	private DelegatedAction d;

	public DelegatedBeatEvent(DelegatedAction action)
	{
		this.d = action;
	}

	public override void doAction ()
	{
		d ();
	}
}
