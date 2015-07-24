using UnityEngine;
using System.Collections;

public class PlayerVerticalMovementAction : AbstractHoldReleaseAction
{
	private Rigidbody playerRigidbody;
	private PlayerVars playervars;
	private float jumpForce = 30f;
	private float timeBetweenJump = 1f;
	private float timerJump = 0;
	private bool jumping;

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
		//experimental
		Debug.Log ("rawr");
	}

	public override void FixedUpdate ()
	{
		base.FixedUpdate ();
		timerJump += Time.fixedDeltaTime;
	}

	void Jump ()
	{
		if (timerJump >= timeBetweenJump) {
			timerJump = 0;
			Vector3 up = playerRigidbody.transform.TransformDirection(Vector3.up);
			//playerRigidbody.MovePosition(transform.position + up * 5);
			playerRigidbody.AddForce(up * jumpForce, ForceMode.Impulse);
		}
		
	}
}

