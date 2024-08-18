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

    

    private PlayerMovement playerMovement;

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.movement;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    public float GetMoveForceMult() { 
        return moveForceMult;
    }
    public float GetTurnRotationMult() { 
        return turnRotationMult;
    }
    public float GetDragMult() { 
        return dragMult;
    }
}
