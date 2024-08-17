using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AT_Spike : AttackTrait
{
    Collider2D spikeCollider;

    private void Awake() {
        spikeCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("enemy")) {
            // other.enemyHealth.takeDamage(damageVal * Time.deltaTime);
        }
    }
}
