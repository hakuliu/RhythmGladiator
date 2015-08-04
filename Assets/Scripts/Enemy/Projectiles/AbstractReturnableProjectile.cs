using UnityEngine;
using System.Collections;

public abstract class AbstractReturnableProjectile : AbstractProjectileScript
{
	protected GameObject source;

	
	public void setSource(GameObject ob) {
		this.source = ob;
	}

	public abstract void ReturnProjectile();
}

