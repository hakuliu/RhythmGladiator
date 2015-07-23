using UnityEngine;
using System.Collections;

public class PlayerHorizontalMovementAction : AbstractHoldReleaseAction
{
	private Vector3 leapMovement;
	private float leapSpeed = .75f;
	public float leapDecay = 1.2f;
	private KeyCode key = KeyCode.Space;
	private Rigidbody playerRigidbody;

	public PlayerHorizontalMovementAction(Rigidbody player) {
		this.playerRigidbody = player;
	}

	public override void doInitialAction ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (h != 0 || v != 0) {
			SetLeapVec (h, v);
		}

	}
	public override void doReleasedAction ()
	{
		//experimental
		Debug.Log ("rawr");
	}
	public override void FixedUpdate ()
	{
		base.FixedUpdate ();

		if (Input.GetKeyDown (key)) {
			Pressed();
		}
		if (Input.GetKeyUp (key)) {
			Released();
		}

		LeapUpdate ();
	}

	void SetLeapVec(float h, float v)
	{
		//if (timerLeap >= timeBetweenLeap && leaping == false && (h != 0 || v != 0)) {
		//	timerLeap = 0f;
			leapMovement = PlayerMovement.GetMovementDirection(h, v);
			leapMovement = leapMovement.normalized * leapSpeed;
		//}
	}

	void LeapUpdate()
	{
		if (leapMovement != Vector3.zero) {
			playerRigidbody.MovePosition(playerRigidbody.transform.position + leapMovement);
			leapMovement /= leapDecay;
			
			if(leapMovement.magnitude <= .1f) {
				leapMovement = Vector3.zero;
			}
		}
	}
}

