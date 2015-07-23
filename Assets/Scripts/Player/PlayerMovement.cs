using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

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

	private PlayerVars globalvars;
	Animator anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	PlayerHorizontalMovementAction leapAction;


	void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		globalvars = player.GetComponent<PlayerVars> ();
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
		leapAction = new PlayerHorizontalMovementAction (playerRigidbody);
	}

	void FixedUpdate()
	{
		if (!globalvars.busy) {
			timerJump = timerJump + Time.deltaTime;
			timerLeap = timerLeap + Time.deltaTime;
			
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");

			bool shift = Input.GetKey (KeyCode.LeftShift);//do right too?

			Walk(h, v);

			leapAction.FixedUpdate();

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

	

	/// <summary>
	/// Gets the movement direction.
	/// we also zero out the y component.
	/// </summary>
	/// <returns>The movement direction.</returns>
	/// <param name="h">horizontal input</param>
	/// <param name="v">vertical input</param>
	public static Vector3 GetMovementDirection(float h, float v) {
		//scrapped because i thought absolute control feels better.
//		Vector3 f = this.transform.TransformDirection (Vector3.forward);
//		Vector3 r = this.transform.TransformDirection (Vector3.right);
//
//		Vector3 rv = f * v + r * h;
//		rv.y = 0;
		Vector3 rv = new Vector3 (h, 0f, v);
		return rv.normalized;
	}
		
	void Turn()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidbody.MoveRotation (newRotation);
		}
	}

	void Anim()
	{
		anim.SetBool ("IsMoving", leaping);
	}

}
