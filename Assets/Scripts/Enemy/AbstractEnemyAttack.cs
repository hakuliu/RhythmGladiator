using UnityEngine;
using System.Collections;

public abstract class AbstractEnemyAttack : MonoBehaviour
{
	protected BeatTracker track;
	protected AudioSource audioSource;
	protected Animator anim;
	protected GameObject player;
	protected DamageManager damager;

	// Use this for initialization
	protected virtual void Start ()
	{
		track = new BeatTracker ();
		assignTrack ();
		audioSource = GetComponent <AudioSource> ();
		anim = GetComponent <Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		GameObject managers = GameObject.FindGameObjectWithTag ("CustomManagers");
		damager = managers.GetComponent<DamageManager> ();
	}
	
	// Update is called once per frame
	protected virtual void Update ()
	{
	
	}
	protected virtual void FixedUpdate() {
		track.FixedUpdate ();
	}
	protected abstract void assignTrack();
}

