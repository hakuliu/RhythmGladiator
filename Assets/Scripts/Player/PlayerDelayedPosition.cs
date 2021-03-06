using UnityEngine;
using System.Collections;
/// <summary>
/// intended to give the enemy targetting a little delay to account for human prediction.
/// we do this by storing a buffer of a few frames (update, not fixed update. idk why i chose this) of player position.
/// </summary>
public class PlayerDelayedPosition : MonoBehaviour
{
	public int bufferFrameCount = 5;
	private Vector3[] buffer;
	private int currentIndex;
	private Transform playerTransform;
	// Use this for initialization
	void Start ()
	{
		playerTransform = GetComponent<Transform> ();
		buffer = new Vector3[bufferFrameCount];
		FillBuffer (playerTransform.position);

	}
	
	// Update is called once per frame
	void Update ()
	{
		this.buffer [currentIndex] = playerTransform.position;
		currentIndex++;
		if (currentIndex >= this.buffer.Length) {
			currentIndex = 0;
		}
	}
	public Vector3 lookupPlayerPositionWithDelay() {
		return lookupPlayerPositionWithDelay (0f);
	}
	public Vector3 lookupPlayerPositionWithDelay(float variance) {
		Vector3 rv = lookupWithIndex (currentIndex + 1);
		variance = Mathf.Abs (variance);//just to make sure
		if (variance != 0) {
			float xvar = Random.Range(-variance, variance);
			float zvar = Random.Range(-variance, variance);

			rv.x += xvar;
			rv.z += zvar;
		}
		return rv;
	}

	Vector3 lookupWithIndex(int index) {
		index ++;
		index %= buffer.Length;
		return this.buffer [index];
	}

	void FillBuffer(Vector3 pos) {
		for (int i = 0; i < this.buffer.Length; i++) {
			this.buffer[i] = pos;
		}
	}
}

