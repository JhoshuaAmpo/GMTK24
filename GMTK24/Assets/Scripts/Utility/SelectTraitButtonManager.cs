using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectTraitButtonManager : MonoBehaviour
{
    Trait trait;
    TextMeshProUGUI textCost;

    private void Awake() {
        trait = GetComponentInChildren<Trait>();
        textCost = transform.Find("Trait Cost").GetComponent<TextMeshProUGUI>();
    }
    private void Start() {
        textCost.text = trait.tCost.ToString();
    }
}
