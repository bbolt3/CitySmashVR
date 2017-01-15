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


    public void OnCollisionEnter (Collision other)
    {
       
        if (other.gameObject.tag != "GameController" && other.gameObject.tag != "Untagged")
        {
            if (this.tag == "Bombs" && other.relativeVelocity.magnitude > 1)
                BombExplosion(other);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BombExplosion(Collision other)
    {
        Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);
        ExplosionDamage();
        Destroy(gameObject);
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
}
