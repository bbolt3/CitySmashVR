using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : DamageableExplodingObject {

	private void Start ()
	{
		Initialize();
	}
	// Update is called once per frame
	private void Update ()
	{
		if (health <= 0 && explosion !=null)
		{
			explosion.BombExplosion();
		}
	}
}
