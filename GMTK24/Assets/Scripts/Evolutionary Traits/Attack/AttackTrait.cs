using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTrait : Trait
{
    [SerializeField]
    protected float sizeMult;
    [SerializeField]
    protected float damageVal;
    [SerializeField]
    protected float abilityCooldown;
}
