using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDamage : Damage {

	public GameObject bloodEffect;
	public bool hasWeapon = false;
	public int threshholdBeforePanic = 50;

	private bool isAlive = true;
	private Animator animator;
	private Navigation navigation;

	// Use this for initialization
	private void Start ()
	{
		animator = gameObject.GetComponent<Animator>();
		navigation = gameObject.GetComponent<Navigation>();
	}
	
	// Update is called once per frame
	private void Update ()
	{
		if (isAlive && health <= 0)
			KillObject();
	}

	private void OnCollisionEnter(Collision other)
	{
		if (isAlive)
			HitObjectWithForce(other);
	}

	private void KillObject()
	{
		var bloodObject = Instantiate(bloodEffect, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
		Destroy(bloodObject.gameObject, 1f);
		isAlive = false;
		animator.SetBool("IsAlive", isAlive);
		Destroy(animator, 1f);
		if (navigation != null)
			navigation.StopNavigation();
	}
}
