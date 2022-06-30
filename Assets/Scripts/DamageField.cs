using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageField : Bullet
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponents<MonoBehaviour>()
                .FirstOrDefault(component => component is IHealthOwner) is IHealthOwner healthOwner)
        {
            healthOwner.TakeDamage(this);
            Die();
        }
    }
}
