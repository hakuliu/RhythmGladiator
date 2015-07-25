using UnityEngine;
using System.Collections;

public class PitFallScript : MonoBehaviour
{
	PlayerHealth phealth;
	// Use this for initialization
	void Start ()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		phealth = player.GetComponent<PlayerHealth> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider c) {
		phealth.Fall ();
	}
}

