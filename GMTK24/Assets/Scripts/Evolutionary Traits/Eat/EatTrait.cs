using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class EatTrait : Trait
{
    [SerializeField]
    protected float pickUpSize;
    [SerializeField]
    protected Vector2 mouthScale;
    [SerializeField]
    protected float nomRate;

    Collider2D eatCollider;
    float mouthSize;

    private void Awake() {
        eatCollider = GetComponent<Collider2D>();
    }

    private void Start() {
        mouthSize = mouthScale.x * mouthScale.y;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("food")) { return; }
        float otherApproxAreaSize = other.transform.lossyScale.x * other.transform.lossyScale.y;
        if(otherApproxAreaSize > mouthSize) {
            Vector3 oLS = other.transform.localScale;
            other.transform.localScale = new(oLS.x - nomRate * Time.fixedDeltaTime, oLS.y - nomRate * Time.fixedDeltaTime);
        } else { 
            // Consume(/* other food amount */);
        }
    }

    protected void Consume(int tp) {
        Debug.Log("Sending message up");
        SendMessageUpwards("GainTraitPoints", tp);
    }
}
