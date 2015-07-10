using UnityEngine;
using System.Collections;
/// <summary>
/// Next state event.
/// requires you to pass in the script instance that has the state machine...this may or may not be unsafe
/// but it seems like game code doesn't give a ** about MVC or code safety so....
/// let's just run with it.
/// 
/// (if i want it to make it safer i might make the statemachines its own class and pass a reference to the object)
/// </summary>
public class NextStateEvent : BeatEvent
{
	IHasSequentialStates machine;
	public NextStateEvent(IHasSequentialStates sm) 
	{
		this.machine = sm;
	}
	public override void doAction ()
	{
		machine.goToNextState ();
	}
}

