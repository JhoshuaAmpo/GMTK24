using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraitManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTraitObjects;

    [Tooltip("How many trait points we have / can spend")]
    public int TraitPoints {get; private set;}

    [SerializeField]
    GameEndManager gem;

    public List<PlayerTraitSlots> TraitSlots {get; private set;}
    public List<Trait> ActiveTraits {get; private set;}
    public List<AttackTrait> AttackTraits {get; private set;}
    public List<MovementTrait> MovementTraits {get; private set;}
    public List<EatTrait> EatTraits {get; private set;}
    
    // Start is called before the first frame update
    void Awake()
    {
        ActiveTraits = new();
        AttackTraits= new();
        MovementTraits= new();
        EatTraits= new();
        TraitSlots = new();

        TraitPoints = 100;

        GetComponentsInChildren<PlayerTraitSlots>(TraitSlots);
        foreach (PlayerTraitSlots ts in TraitSlots)
        {
            foreach (GameObject t in allTraitObjects)
            {
                ts.SpawnTrait(t);
            }
        }

        foreach (PlayerTraitSlots ts in TraitSlots)
        {
            Trait trait = ts.GetCurrentTrait();
            if(trait != null) {
                ActiveTraits.Add(trait);
                ts.EnableCurrentTrait();
            }         
        }

        foreach (Trait t in ActiveTraits)
        {
            if (t is AttackTrait) {
                AttackTraits.Add(t as AttackTrait);
            }
            if (t is MovementTrait) {
                MovementTraits.Add(t as MovementTrait);
            }
            if (t is EatTrait) {
                EatTraits.Add(t as EatTrait);
            }
        }
    }

    public void GainTraitPoints(int tp) {
        Debug.Log("TP gained!");
        TraitPoints += tp;
        gem.UpdateBar(TraitPoints);
    }

    public void SwapTrait(int traitSlotIndex, GameObject trait) {
        TraitSlots[traitSlotIndex].SwapTrait(trait);
    }

    public int GetTotalTraitCost() {
        int total = 0;
        foreach (Trait trait in ActiveTraits) {
            total += trait.tCost;
        }
        return total;
    }

    public void ActivateEveryAttack() {
        foreach (var aT in AttackTraits)
        {
            aT.ActivateAttack();
        }
    }
}
