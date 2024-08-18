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
    
    private float finalForwardForce;
    private float finalBackwardForce;
    private float finalRotTorque;
    private float finalLinearDrag;

    Rigidbody2D rb;
    PlayerControls pc;

    [Header("Calculating Movement Traits")]
    PlayerTraitManager ptm;
    float avgMoveForceMult = 1;
    float avgTurnRotationMult = 1;
    float avgDragMult = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        pc = new();
        ptm = GetComponentInChildren<PlayerTraitManager>();

        
    }

    private void Start() {
        UpdateTraitValues();
    }

    private void OnEnable() {
        pc.Movement.Enable();
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
        // Current way of getting multipliers: average it all out then multiply to base
        if (ptm.MovementTraits.Count == 0) {
            avgMoveForceMult = 0;
            avgTurnRotationMult = 0;
            avgDragMult = 1;
        } else {
            foreach (MovementTrait mT in ptm.MovementTraits)
            {
                avgMoveForceMult += mT.GetMoveForceMult;
                avgTurnRotationMult += mT.GetTurnRotationMult;
                avgDragMult += mT.GetDragMult;
            }
            avgMoveForceMult /= (float)ptm.MovementTraits.Count + 1;
            avgTurnRotationMult /= (float)ptm.MovementTraits.Count + 1;
            avgDragMult /= (float)ptm.MovementTraits.Count + 1;
        }
        finalForwardForce = baseForwardForce * avgMoveForceMult;
        finalBackwardForce = baseBackwardForce * avgMoveForceMult;
        finalRotTorque = baseRotTorque * avgTurnRotationMult;
        finalLinearDrag = rb.drag * avgDragMult;

        rb.drag = finalLinearDrag;
    }
}
