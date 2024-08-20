using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CowardEnemyBehvaior : MonoBehaviour
{
    [SerializeField]
    float moveForce;

    // Time between taking an action
    [SerializeField]
    float actionCooldown;
    float actionTimer;

    Rigidbody2D rb;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (actionTimer <= 0) {
            Wander();
            actionTimer = actionCooldown;
        }
        actionTimer -= Time.fixedDeltaTime;
    }
    
    private void Wander() {
        Vector2 dir = Random.insideUnitCircle.normalized;
        float angle = Vector2.Angle(Vector2.up, dir);
        transform.rotation = Quaternion.Euler(0,0, angle);
        rb.AddForce(transform.up * moveForce,ForceMode2D.Impulse);
    }
}
