using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public GameObject explosionPrefab;
    public GameObject smokePrefab;
    public GameObject bloodPrefab;
    public GameObject primaryDebrisPrefab;
    public GameObject secondaryDebrisPrefab;
    public float blastRadius = 10f;
    public float blastPower = 20f;
    public float blastUpwardsModifier = 3f;
    public float explosionDelay = 1f;

    private GameObject instantiatedExplosion;
    private GameObject instantiatedSmoke;
    private bool collisionStarted = false;

    public void VehicleExplosion()
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
    public void BombExplosion()
    {
        DoExplosion();
        ExplosionDamage();
        Destroy(gameObject);
    }

    public void HouseExplosion(float addedForce)
    {
        if (!collisionStarted)
        {
            collisionStarted = true;
            if (primaryDebrisPrefab != null)
                CreateDebrisAndAddForce(primaryDebrisPrefab, addedForce);
            if (secondaryDebrisPrefab != null)
                CreateDebrisAndAddForce(secondaryDebrisPrefab, addedForce);
        }
    }

    private void CreateDebrisAndAddForce(GameObject generatedDebris, float addedForce)
    {
        var debris = Instantiate(generatedDebris, gameObject.transform.position, primaryDebrisPrefab.transform.rotation);
        var rigidbody = debris.GetComponent<Rigidbody>();
        if (rigidbody != null)
            rigidbody.AddExplosionForce(addedForce, debris.transform.position, blastRadius);
    }
    private void DoExplosion()
    {
        instantiatedExplosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        SendRaycast();
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
    private void SendRaycast()
    {
        var hits = Physics.SphereCastAll(instantiatedExplosion.transform.position, blastRadius, transform.forward);
        foreach(var hit in hits)
        {
            var damage = blastPower / hit.distance;
            hit.transform.SendMessage("HitByRaycast", damage, SendMessageOptions.DontRequireReceiver);
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
