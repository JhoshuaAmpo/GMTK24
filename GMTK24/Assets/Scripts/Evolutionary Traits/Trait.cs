using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait : MonoBehaviour
{
    public enum TraitType { none, attack, movement, eat }
    public enum TraitTier { none, one = 1, two = 2, three = 3}
    public enum TraitName { none, flagella, propeller, spike, gun, mouth, straw}

    public TraitType tType = TraitType.none;
    public TraitTier tTier = TraitTier.none;
    public TraitName tName = TraitName.none;

    protected virtual void Awake() { 
        CheckTraitCodeForNone();
    }
    protected virtual void Start() { }
    protected virtual void Update() { }
    
    public string GetTraitCode() {
        CheckTraitCodeForNone();
        return tType.ToString() + tTier.ToString() + tName.ToString();
    }

    public bool CompareTraitCode(TraitType tTy, TraitTier tTi, TraitName tNa) {
        CheckTraitCodeForNone();
        return tTy == tType && tTi == tTier && tNa == tName;
    }

    private void CheckTraitCodeForNone() {
        if (tType == TraitType.none) 
        {
            Debug.LogError(name + ": TraitType set to none");
        }
        if (tTier == TraitTier.none) {
            Debug.LogError(name + ": TraitTier set to none");
        }
        if (tName == TraitName.none) {
            Debug.LogError(name + ": TraitName set to none");
        }
    }
}
