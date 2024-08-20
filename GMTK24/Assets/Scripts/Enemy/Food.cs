using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour
{
    [SerializeField]
    private int traitPoints = 1;

    private void OnEnable() {
        if (transform.localScale.x > 100f) {
            transform.localScale = new Vector3(100f, 100f, 100f);
        }
    }
    public int ConsumeFood() {
        // Debug.Log("Food has been consumed");
        gameObject.SetActive(false);
        return traitPoints;
    }

    public void SetTraitPoints(int val) {
        if (val <= 0) {
            traitPoints = 1;
        } else {
            traitPoints = val;
        }
    }
}
