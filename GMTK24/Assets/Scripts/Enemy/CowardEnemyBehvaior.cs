using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CowardEnemyBehvaior : MonoBehaviour
{
    [SerializeField]
    Vector2 sizeScaleRange;

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

    private void OnEnable() {
        actionTimer = 0f;
        float lS = Random.Range(sizeScaleRange.x, sizeScaleRange.y);
        transform.localScale = new Vector3(lS, lS,lS);
        LevelUp();
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

    private void LevelUp() {
        if(sizeScaleRange.y <= 50f) {
            sizeScaleRange *= 1.25f;
        }

        moveForce += 1f;
        moveForce = Mathf.Clamp(moveForce, 3, 200);

        actionCooldown -= 0.1f;
        actionCooldown = Mathf.Clamp(actionCooldown, 1,10);
    }
}
