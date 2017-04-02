using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleDamage : DamageableExplodingObject {

    private Navigation navigation;
    private bool isCompleted = false;

    private void Start()
    {
        navigation = gameObject.GetComponent<Navigation>();
        Initialize();
    }

	// Update is called once per frame
	private void Update ()
    {
        if (health <= 0 && !isCompleted)
        {
            isCompleted = true;
            if (navigation != null)
                navigation.StopNavigation();
            if (explosion != null)
                explosion.VehicleExplosion();
        }
	}
}
