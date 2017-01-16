using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {

    public GameObject bloodEffect;
	public int health = 100;
	public bool hasWeapon = false;
	public int threshholdBeforePanic = 50;
    public float armorRating = .1f;

    private bool isAlive = true;
    private Animator animator;

	// Use this for initialization
	private void Start ()
    {
        animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
		if (isAlive && health <= 0)
        {
            KillObject();
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if (isAlive)
        {
            var velMag = other.relativeVelocity.magnitude;
            health -= (int)(velMag / armorRating);
        } 
    }

    private void KillObject()
    {
        var bloodObject = Instantiate(bloodEffect, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
        Destroy(bloodObject.gameObject, 1f);
        isAlive = false;
        animator.SetBool("IsAlive", isAlive);
        Destroy(animator, 1f);
    }
}
