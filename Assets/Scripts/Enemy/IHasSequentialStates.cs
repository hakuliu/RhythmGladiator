
using System;
/// <summary>
/// Indicates that classes implementing this interface
/// 1. has a finite statemachine.
/// 2. has a sequential order or pattern in state transition.
/// </summary>
public interface IHasSequentialStates
{
	void goToNextState();
}