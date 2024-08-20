using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour
{
    private int traitPoints = 1;
    public int ConsumeFood() {
        // Debug.Log("Food has been consumed");
        gameObject.SetActive(false);
        return traitPoints;
    }

    public void SetTraitPoints(int val) {
        traitPoints = val;
    }
}
