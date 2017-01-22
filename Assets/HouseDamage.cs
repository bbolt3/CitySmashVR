using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDamage : DamageableExplodingObject {

	private bool isDestroyed = false;

	private void Start()
	{
		Initialize();
	}

	// Update is called once per frame
	private void Update ()
	{
		if (!isDestroyed && health <= 0)
		{
			isDestroyed = true;
			Destroy(gameObject);
			explosion.HouseExplosion(5f);
		}
	}
}
