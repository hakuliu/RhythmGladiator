
using System;
/// <summary>
/// a package that has a bunch of static methods that conveniently constructs commonly used BeatEvents.
/// so that you can do inline things and not worry about constructors.
/// </summary>
public class CommonEventFactory
{
	/// <summary>
	/// Gets an event that makes the state machine goes to its "next" state
	/// </summary>
	/// <returns>The next state event.</returns>
	/// <param name="statemachine">the script instance that contains the state machine</param>
	public static BeatEvent getNextStateEvent(IHasSequentialStates statemachine)
	{
		return new NextStateEvent(statemachine);
	}

	public static BeatEvent getDelegatedEvent(DelegatedBeatEvent.DelegatedAction del)
	{
		return new DelegatedBeatEvent(del);
	}

	public static BeatEvent getNoOp()
	{
		return new NoopEvent ();
	}
}