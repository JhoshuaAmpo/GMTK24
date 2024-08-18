using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField]
    private float baseForwardForce;
    [SerializeField]
    private float baseBackwardForce;
    [SerializeField]
    private float baseRotTorque;
    [SerializeField]
    private float baseDrag;
    
    private float finalForwardForce;
    private float finalBackwardForce;
    private float finalRotTorque;
    private float finalLinearDrag;

    Rigidbody2D rb;
    PlayerControls pc;

    [Header("Calculating Movement Traits")]
    PlayerTraitManager ptm;
    float combinedMoveForceMult = 1;
    float combinedTurnRotationMult = 1;
    float combinedDragMult = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        pc = new();
        ptm = GetComponentInChildren<PlayerTraitManager>();
    }

    private void OnEnable() {
        pc.Movement.Enable();
    }
    private void Start() {
        rb.drag = baseDrag;
        UpdateTraitValues();
    }

    private void OnDisable() {
        pc.Movement.Disable();
    }

    private void Update() {
        MoveForward();
        Rotate();
    }

    private void MoveForward()
    {
        int dir = (int)pc.Movement.Forward.ReadValue<float>();
        if (dir == 1) {
            rb.AddForce(transform.up * finalForwardForce, ForceMode2D.Force);
        }
        if (dir == -1) {
            rb.AddForce(-transform.up * finalBackwardForce, ForceMode2D.Force);
        }
        // Debug.Log("Force: " +  rb.GetAccumulatedForce());
    }

    private void Rotate()
    {
        int dir = (int)pc.Movement.Rotate.ReadValue<float>();
        rb.AddTorque(-dir * finalRotTorque,ForceMode2D.Force);
    }

    // Call this funciton after swapping traits
    public void UpdateTraitValues() {
        if (ptm.MovementTraits.Count == 0) {
            combinedMoveForceMult = 0;
            combinedTurnRotationMult = 0;
            combinedDragMult = 1;
        } else {
            // Way 1 Average out mult
            // foreach (MovementTrait mT in ptm.MovementTraits)
            // {
            //     combinedMoveForceMult += mT.GetMoveForceMult();
            //     combinedTurnRotationMult += mT.GetTurnRotationMult();
            //     combinedDragMult += mT.GetDragMult();
            // }
            // // combinedMoveForceMult /= (float)ptm.MovementTraits.Count + 1;
            // // combinedTurnRotationMult /= (float)ptm.MovementTraits.Count + 1;
            // // combinedDragMult /= (float)ptm.MovementTraits.Count + 1;

            // Way 2 Mult all the mults
            foreach (MovementTrait mT in ptm.MovementTraits)
            {
                combinedMoveForceMult *= mT.GetMoveForceMult();
                combinedTurnRotationMult *= mT.GetTurnRotationMult();
                combinedDragMult *= mT.GetDragMult();
            }
        }
        finalForwardForce = baseForwardForce * combinedMoveForceMult;
        finalBackwardForce = baseBackwardForce * combinedMoveForceMult;
        finalRotTorque = baseRotTorque * combinedTurnRotationMult;
        finalLinearDrag = rb.drag * combinedDragMult;

        rb.drag = finalLinearDrag;
    }
}
