using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class AttackTrait : Trait
{
    [SerializeField]
    [Tooltip("1 = default size")]
    protected float sizeMult;
    [SerializeField]
    [Tooltip("Damage done on contact or per second")]
    protected float damageVal;
    [SerializeField]
    [Tooltip("In seconds")]
    protected float abilityCooldown;
    protected float abilityTimer = 0;

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.attack;
        abilityTimer = 0;
    }

    protected override void Update()
    {
        base.Update();
        if (abilityTimer > 0f) {
            abilityTimer -= Time.deltaTime;
            abilityTimer = Mathf.Clamp(abilityTimer, 0f, abilityCooldown);
        }
    }

    protected void ResetTimer() {
        abilityTimer = abilityCooldown;
    }

    public abstract void ActivateAttack();
}
