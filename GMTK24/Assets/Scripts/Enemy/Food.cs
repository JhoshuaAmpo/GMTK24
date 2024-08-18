using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour
{
    [Range(0,100)]
    public int traitPoints = 0;

    public int ConsumeFood() {
        // Destroy this and enemy
        return traitPoints;
    }
}
