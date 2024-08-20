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

    PlayerMovement playerMovement;
    
    void Awake()
    {
        // TraitPoints = 200;

        ActiveTraits = new();
        AttackTraits = new();
        MovementTraits = new();
        EatTraits = new();
        TraitSlots = new();

        // GetComponentsInChildren<PlayerTraitSlots>(TraitSlots);
        // // foreach (PlayerTraitSlots ts in TraitSlots)
        // // {
        // //     foreach (GameObject t in allTraitObjects)
        // //     {
        // //         ts.SpawnTrait(t);
        // //     }
        // // }

        // TraitSlots[0].SpawnTrait(allTraitObjects[3]);
        // TraitSlots[3].SpawnTrait(allTraitObjects[4]);
        // TraitSlots[1].SpawnTrait(allTraitObjects[0]);
        // TraitSlots[2].SpawnTrait(allTraitObjects[0]);
        // TraitSlots[4].SpawnTrait(allTraitObjects[0]);
        // TraitSlots[5].SpawnTrait(allTraitObjects[0]);

        // foreach (PlayerTraitSlots ts in TraitSlots)
        // {
        //     ts.transform.GetChild(0).gameObject.SetActive(true);
        // }


        // playerMovement = transform.root.GetComponent<PlayerMovement>();
        // UpdateTraitLists();
    }

    private void UpdateTraitLists()
    {
        foreach (PlayerTraitSlots ts in TraitSlots)
        {
            Trait trait = ts.GetCurrentTrait();
            if (trait != null)
            {
                ActiveTraits.Add(trait);
                ts.EnableCurrentTrait();
            }
        }

        foreach (Trait t in ActiveTraits)
        {
            if (t is AttackTrait)
            {
                AttackTraits.Add(t as AttackTrait);
            }
            if (t is MovementTrait)
            {
                MovementTraits.Add(t as MovementTrait);
            }
            if (t is EatTrait)
            {
                EatTraits.Add(t as EatTrait);
            }
        }
    }

    public void GainTraitPoints(int tp) {
        // Debug.Log("TP gained!");
        TraitPoints += tp;
        gem.UpdateBar(TraitPoints);
    }

    public void SwapTrait(int traitSlotIndex, GameObject trait) {
        TraitSlots[traitSlotIndex].SwapTrait(trait);
        playerMovement.UpdateTraitValues();
        UpdateTraitLists();
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
