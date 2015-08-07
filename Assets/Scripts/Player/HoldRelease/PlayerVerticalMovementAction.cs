using UnityEngine;
using System.Collections;

public class PlayerVerticalMovementAction : AbstractHoldReleaseAction
{
	public float jumpForce = 1.5f;
	public float jumpDecay = 1.5f;
	private Rigidbody playerRigidbody;
	private PlayerVars playervars;
	private float timeBetweenJump = 1f;
	private float timerJump = 0;
	private bool jumping;
	private Vector3 vel;

	public PlayerVerticalMovementAction(Rigidbody player, PlayerVars vars) : base(KeyCode.E) {
		this.playerRigidbody = player;
		this.playervars = vars;
	}

	protected override void doInitialAction ()
	{
		Jump();
	}
	protected override void doReleasedAction ()
	{
		Stomp ();
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();
		timerJump += Time.fixedDeltaTime;
		JumpUpdate ();
		if (pressed) {
			playerRigidbody.useGravity = false;
		} else {
			playerRigidbody.useGravity = true;
		}

	}
	void JumpUpdate() {
		if (jumping) {
			playerRigidbody.MovePosition(playerRigidbody.transform.position + vel);
			vel /= jumpDecay;
			if(vel.magnitude <= .2) {
				vel = Vector3.zero;
				jumping = false;
			}
		}
	}
	void Jump ()
	{
		if (timerJump >= timeBetweenJump) {
			timerJump = 0;
			Vector3 up = playerRigidbody.transform.TransformDirection(Vector3.up);
			vel = up * jumpForce;
			jumping = true;
		}
		
	}
	void Stomp() {
		Vector3 tempos = playerRigidbody.transform.position;
		tempos.y = 0f;
		playerRigidbody.MovePosition (tempos);
	}
}

