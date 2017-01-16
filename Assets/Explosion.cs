using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject smokePrefab;
    public GameObject bloodPrefab;
    public float blastRadius = 10f;
    public float blastPower = 20f;
    public float blastUpwardsModifier = 3f;
    public float explosionDelay = 1f;

    private GameObject instantiatedExplosion;
    private GameObject instantiatedSmoke;
    private bool collisionStarted = false;

    public void OnCollisionEnter (Collision other)
    {
       
        if (other.gameObject.tag != "GameController" && other.gameObject.tag != "Untagged")
        {
            if (this.tag == "Bombs" && other.relativeVelocity.magnitude > 1)
                BombExplosion(other);
            else if (this.tag == "Vehicle" && other.relativeVelocity.magnitude > 1)
                VehicleExplosion(other);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void VehicleExplosion(Collision other)
    {
        if (!collisionStarted)
        {
            collisionStarted = true;
            instantiatedSmoke = Instantiate(smokePrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            StartCoroutine(DelayedExplosion(explosionDelay));
        }
        else
        {
            instantiatedSmoke.gameObject.transform.position = gameObject.transform.position;
        }
    }
    private void BombExplosion(Collision other)
    {
        DoExplosion();
        ExplosionDamage();
        Destroy(gameObject);
    }
    private void DoExplosion()
    {
        instantiatedExplosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(instantiatedExplosion.gameObject, 4f);
    }
    private void ExplosionDamage()
    {
        var explosionPosition = transform.position;
        var colliders = Physics.OverlapSphere(explosionPosition, blastRadius);
        foreach (var hit in colliders)
        {
            var rigidBody = hit.GetComponent<Rigidbody>();
            if (rigidBody != null && rigidBody.gameObject.layer != LayerMask.NameToLayer("Camera"))
                rigidBody.AddExplosionForce(blastPower, explosionPosition, blastRadius, blastUpwardsModifier);
        }
    }
    private IEnumerator DelayedExplosion(float duration)
    {
        yield return new WaitForSeconds(duration);
        DoExplosion();
        Destroy(instantiatedSmoke.gameObject);
        Destroy(gameObject);
    }
}
