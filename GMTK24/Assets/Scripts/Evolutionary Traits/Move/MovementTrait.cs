using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementTrait : Trait
{
    [SerializeField]
    protected float moveForceMult;
    [SerializeField]
    protected float turnRotationMult;
    [SerializeField]
    protected float dragMult;

    public float GetMoveForceMult;
    public float GetTurnRotationMult;
    public float GetDragMult;
}
