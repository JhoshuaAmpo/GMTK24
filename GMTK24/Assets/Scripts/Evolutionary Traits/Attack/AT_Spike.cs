using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AT_Spike : AttackTrait
{
    Collider2D spikeCollider;

    [Header("Ability Var")]
    Vector3 originalScale;
    float originalY;
    private const float duration = 2f;

    protected override void Awake() {
        base.Awake();
        spikeCollider = GetComponent<Collider2D>();
        originalScale = transform.localScale;
        originalY = transform.localPosition.y;
    }

    protected override void Update() {
        base.Update();
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

    public override void ActivateAttack()
    {
        // Debug.Log("Attack activated!");
        // // if (abilityTimer > 0) { return; }
        // // ResetTimer();
        // transform.localScale = transform.localScale * sizeMult;
        // // float newY = transform.localPosition.y + transform.localScale.y/ 4 * transform.forward.y;
        // // transform.localPosition = new Vector3(transform.position.x, newY, transform.position.z);
        //  // StartCoroutine(DelayResetSpike());
    }

    private IEnumerator DelayResetSpike() {
        yield return new WaitForSeconds(duration);
        ResetSpike();
    }

    private void ResetSpike() {
        transform.localScale = originalScale;
        transform.localPosition = new(transform.localPosition.x, originalY, transform.localPosition.z);
    }
}
