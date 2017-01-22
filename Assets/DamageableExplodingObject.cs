using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableExplodingObject : Damage
{

	protected Explosion explosion;

	// Use this for initialization
	protected void Initialize()
	{
		explosion = gameObject.GetComponent<Explosion>();
	}

	protected void OnCollisionEnter(Collision other)
	{
		HitObjectWithForce(other);
	}
}
	
