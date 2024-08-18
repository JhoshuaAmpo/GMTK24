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

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.eat;
        eatCollider = GetComponent<Collider2D>();
    }

    protected override void Start() {
        base.Start();
        mouthSize = mouthScale.x * mouthScale.y;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Food")) { return; }
        float otherApproxAreaSize = other.transform.lossyScale.x * other.transform.lossyScale.y;
        if(otherApproxAreaSize > mouthSize) {
            Vector3 oLS = other.transform.localScale;
            other.transform.localScale = new(oLS.x - nomRate * Time.fixedDeltaTime, oLS.y - nomRate * Time.fixedDeltaTime);
        } else { 
            Consume(other.GetComponent<Food>().ConsumeFood());
        }
    }

    protected void Consume(int tp) {
        Debug.Log("Sending message up");
        SendMessageUpwards("GainTraitPoints", tp);
    }
}
