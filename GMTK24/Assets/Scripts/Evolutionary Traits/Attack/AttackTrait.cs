using System;
using System.Collections;
using System.Collections.Generic;
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

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.attack;
    }

    protected abstract void ActivateAttack();
}
