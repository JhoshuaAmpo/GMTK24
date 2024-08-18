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

    public float GetMoveForceMult { get; private set; }
    public float GetTurnRotationMult { get; private set; }
    public float GetDragMult { get; private set; }

    private PlayerMovement playerMovement;

    protected override void Awake() {
        base.Awake();
        tType = Trait.TraitType.movement;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        GetMoveForceMult = moveForceMult;
        GetTurnRotationMult = turnRotationMult;
        GetDragMult = dragMult;
    }
}
