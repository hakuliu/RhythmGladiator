using UnityEngine;
using System.Collections;

public class GroundSpikeProjectile : AbstractProjectileScript, IHasSequentialStates
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public override void setTrackDelaysAndStart (float[] delays)
	{
		//
	}

	void IHasSequentialStates.goToNextState() {

	}
}

