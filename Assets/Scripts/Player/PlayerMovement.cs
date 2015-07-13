using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float leapSpeed = .75f;
	public float leapDecay = 1.2f;
	public float walkSpeed = .3f;
	public float turnrate = .05f;


	float timeBetweenLeap = .33f;//s
	float timerLeap = 0;
	bool leaping;

	float timeBetweenJump = 1f;
	float timerJump = 0;
	float jumpForce = 30f;
	bool jumping;

	Vector3 movementh;
	Vector3 movementv;

	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();

	}

	void FixedUpdate()
	{
		timerJump = timerJump + Time.deltaTime;
		timerLeap = timerLeap + Time.deltaTime;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		bool j = Input.GetKey (KeyCode.Space);
		bool shift = Input.GetKey (KeyCode.LeftShift);//do right too?


		if (j) {
			Jump();
		}
		if (shift) {
			Debug.Log("shift down");
			Walk(h, v);
		} else {
			this.SetLeapVec (h, v);
			
			if (leaping) {
				LeapMove();
			}
		}

		this.Anim ();
		this.Turn ();
	}

	void Jump ()
	{
		if (timerJump >= timeBetweenJump) {
			timerJump = 0;
			Vector3 up = transform.TransformDirection(Vector3.up);
			//playerRigidbody.MovePosition(transform.position + up * 5);
			playerRigidbody.AddForce(up * jumpForce, ForceMode.Impulse);
		}

	}
	void Walk(float h, float v) {
		Vector3 movement = GetMovementDirection (h, v);
		movement = movement.normalized * walkSpeed;
		playerRigidbody.MovePosition(transform.position + movement);
	}
	void LeapMove()
	{
		playerRigidbody.MovePosition(transform.position + movementh);
		movementh /= leapDecay;
		
		if(movementh.magnitude <= .1f) {
			movementh = Vector3.zero;
			leaping = false;
		}
	}

	void SetLeapVec(float h, float v)
	{
		if (timerLeap >= timeBetweenLeap && leaping == false && (h != 0 || v != 0)) {
			timerLeap = 0f;
			movementh = GetMovementDirection(h, v);
			movementh = movementh.normalized * leapSpeed;
			leaping = true;
		}
	}

	/// <summary>
	/// Gets the movement direction. with respect to the player's facing
	/// we also zero out the y component.
	/// </summary>
	/// <returns>The movement direction.</returns>
	/// <param name="h">horizontal input</param>
	/// <param name="v">vertical input</param>
	Vector3 GetMovementDirection(float h, float v) {

		Vector3 f = this.transform.TransformDirection (Vector3.forward);
		Vector3 r = this.transform.TransformDirection (Vector3.right);

		Vector3 rv = f * v + r * h;
		rv.y = 0;
		Debug.Log (rv.normalized);
		return rv.normalized;
	}
		
	void Turn()
	{
		float mousemove = Input.GetAxis ("Mouse X");
		if (mousemove != 0) {
			Vector3 forward = playerRigidbody.transform.TransformDirection(Vector3.forward);
			Vector3 right = playerRigidbody.transform.TransformDirection(Vector3.right) * mousemove * turnrate;
			Vector3 playerToMouse = forward + right;
			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Anim()
	{
		anim.SetBool ("IsMoving", leaping);
	}

}
