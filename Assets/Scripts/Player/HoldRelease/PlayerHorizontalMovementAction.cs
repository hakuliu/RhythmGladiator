using UnityEngine;
using System.Collections;

public class PlayerHorizontalMovementAction : AbstractHoldReleaseAction
{
	private Vector3 leapMovement;
	private float leapSpeed = .75f;
	public float leapDecay = 1.2f;
	private Rigidbody playerRigidbody;
	private PlayerVars playervars;
	private int floorMask;
	private float camRayLength = 100f;

	private float holdTimer;

	public PlayerHorizontalMovementAction(Rigidbody player, PlayerVars vars) : base(KeyCode.Space) {
		this.playerRigidbody = player;
		this.playervars = vars;
		this.floorMask = LayerMask.GetMask ("Floor");
	}

	protected override void doInitialAction ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if (h != 0 || v != 0) {
			SetLeapVec (h, v);
		}

	}
	protected override void doReleasedAction ()
	{
		Blink ();
	}
	public override void FixedUpdate ()
	{
		base.FixedUpdate ();
		LeapUpdate ();
	}
	protected override void applyHoldPhysicsBehavior ()
	{
		playervars.playerMovementModifier = .1f;
	}
	protected override void removeHoldPhysicsBehavior ()
	{
		playervars.playerMovementModifier = 1f;
	}

	protected override void BeginHoldEffect ()
	{
		base.BeginHoldEffect ();
		holdTimer = 0;
		Projector indicator = playervars.playerRangeIndicator;
		indicator.orthographicSize = playervars.blinkRange;
		indicator.transform.position = new Vector3(playerRigidbody.transform.position.x , playervars.RangeIndicatorHeight, playerRigidbody.transform.position.z);
		indicator.enabled = true;
	}

	protected override void EndHoldEffect ()
	{
		base.EndHoldEffect ();
		Projector indicator = playervars.playerRangeIndicator;
		indicator.enabled = false;
	}

	protected override void HoldEffectUpdate ()
	{
		base.HoldEffectUpdate ();
		holdTimer += Time.deltaTime;
		Projector indicator = playervars.playerRangeIndicator;
		float optimalThresh = (this.releaseOptimalBeat - holdBeginThreshold) * BeatManager.TickTime;
		float releaseThresh = postReleaseVariance * BeatManager.TickTime;
		if (holdTimer < optimalThresh) {
			float ratio = holdTimer / optimalThresh;
			indicator.transform.position = new Vector3 (playerRigidbody.transform.position.x, playervars.RangeIndicatorHeight * (1 - ratio), playerRigidbody.transform.position.z);
		} else if (holdTimer < optimalThresh + releaseThresh) {
			float ratio = (holdTimer - optimalThresh) / releaseThresh;
			indicator.transform.position = new Vector3 (playerRigidbody.transform.position.x, playervars.RangeIndicatorHeight * (ratio), playerRigidbody.transform.position.z);
		} else {
			EndHoldEffect();
		}
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
	void Blink() {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - playerRigidbody.transform.position;

			Vector3 toTarget = playerToMouse.normalized * playervars.blinkRange;
			Vector3 targetPos = toTarget + playerRigidbody.transform.position;
			playerRigidbody.MovePosition(targetPos);
		}
	}
}

