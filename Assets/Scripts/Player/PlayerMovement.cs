using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = .75f;
	public float speedDecay = 1.2f;

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

	GameObject floor;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();

		floor = GameObject.FindGameObjectWithTag ("Floor");
	}

	void FixedUpdate()
	{
		timerJump = timerJump + Time.deltaTime;
		timerLeap = timerLeap + Time.deltaTime;

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		bool j = Input.GetKey (KeyCode.Space);

		if (j) {
			Jump();
		}
		
		this.SetMoveVec (h, v);
		
		if (leaping) {
			MoveManual();
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
	void MoveManual()
	{
		playerRigidbody.MovePosition(transform.position + movementh);
		movementh /= speedDecay;
		
		if(movementh.magnitude <= .1f) {
			movementh = Vector3.zero;
			leaping = false;
		}
	}

	void SetMoveVec(float h, float v)
	{
		if (timerLeap >= timeBetweenLeap && leaping == false && (h != 0 || v != 0)) {
			timerLeap = 0f;
			movementh.Set (h, 0f, v);
			movementh = movementh.normalized * speed;
			leaping = true;
		}
	}

	void Turn()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);
		}
	}

	void Anim()
	{
		anim.SetBool ("IsMoving", leaping);
	}

}
