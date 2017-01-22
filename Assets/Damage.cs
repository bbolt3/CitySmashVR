using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    public int health = 100;
    public float armorRating = .1f;

    private int fear = 0;

    public void HitByRaycast(float damage)
    {
        if (health > 0)
        {
            if (armorRating > 0f)
            {
                health -= (int)(damage / armorRating);
            }
            else
                health -= (int)damage;

            fear += (int)damage;
        }
    }

    protected void HitObjectWithForce(Collision other)
    {
        if (other.relativeVelocity.magnitude > 10f)
        {
            var velMag = other.relativeVelocity.magnitude;
            health -= (int)(velMag / armorRating);

        }
    }
}
