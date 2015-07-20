using UnityEngine;
using System.Collections;

public class DecayingDebris : MonoBehaviour
{
	public float decayTimeSeconds = 1f;
	float timer;
	// Use this for initialization
	void Start ()
	{
		timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(timer >= decayTimeSeconds) {
			Destroy(gameObject);
		}
		timer += Time.deltaTime;
	}
}

