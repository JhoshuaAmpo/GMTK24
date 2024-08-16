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
    private float forwardForce;
    [SerializeField]
    private float backwardForce;
    [SerializeField]
    private float rotTorque;
    Rigidbody2D rb;
    PlayerControls pc;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        pc = new();
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
            rb.AddForce(transform.up * forwardForce, ForceMode2D.Force);
        }
        if (dir == -1) {
            rb.AddForce(-transform.up * backwardForce, ForceMode2D.Force);
        }
        // Debug.Log("Force: " +  rb.GetAccumulatedForce());
    }

    private void Rotate()
    {
        int dir = (int)pc.Movement.Rotate.ReadValue<float>();
        rb.AddTorque(-dir * rotTorque,ForceMode2D.Force);
    }
}
