using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AT_Spike : AttackTrait
{
    Collider2D spikeCollider;

    protected override void Awake() {
        base.Awake();
        spikeCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageVal);
        }
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageVal*Time.fixedDeltaTime);
        }
    }

    protected override void ActivateAttack()
    {
        Debug.Log("Spike attack activated!");
    }
}
