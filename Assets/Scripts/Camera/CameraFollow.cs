using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;
	public float offsetBack = 5f;
	public float offsetHeight = 2f;

	Vector3 offset;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		Vector3 targetpos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetpos, smoothing * Time.deltaTime);
	}
}
