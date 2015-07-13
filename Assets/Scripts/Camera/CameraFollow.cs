using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;
	public float offsetBack = 25f;
	public float offsetHeight = 10f;

	Vector3 offset;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		Vector3 targetpos = target.TransformDirection (Vector3.forward);
		targetpos = -1 * targetpos.normalized;//pretty sure it already is...w/e
		targetpos *= offsetBack;
		targetpos.y += offsetHeight;
		targetpos += target.position;

		Vector3 lookto = target.position - targetpos;
		Quaternion targetrot = Quaternion.LookRotation (lookto);

		transform.position = Vector3.Lerp (transform.position, targetpos, smoothing * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetrot, smoothing * Time.deltaTime);
	}
}
