using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class EatTrait : Trait
{
    [SerializeField]
    protected float pickUpSize;
    [SerializeField]
    protected float nomRate;

    Collider2D eatCollider;
    protected float mouthSize;

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.eat;
        eatCollider = GetComponent<Collider2D>();
    }

    protected override void Start() {
        base.Start();
        if (mouthSize == 0f) {
            Debug.LogWarning("Mouth Size set to 0");
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Food")) { return; }
        // Debug.Log("Nomming on some food!");
        float otherApproxAreaSize = other.transform.lossyScale.x * other.transform.lossyScale.y;
        if(otherApproxAreaSize > mouthSize) {
            Vector3 oLS = other.transform.localScale;
            other.transform.localScale = new(oLS.x - nomRate * Time.fixedDeltaTime, oLS.y - nomRate * Time.fixedDeltaTime);
        } else { 
            Consume(other.GetComponent<Food>().ConsumeFood());
        }
    }

    protected void Consume(int tp) {
        // Debug.Log("Sending message up");
        SendMessageUpwards("GainTraitPoints", tp);
    }
}
