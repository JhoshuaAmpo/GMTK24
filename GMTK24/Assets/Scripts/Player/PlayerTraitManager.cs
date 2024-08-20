using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraitManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTraitObjects;

    public int TraitPoints {get; private set;}

    PlayerTraitSlots[] traitSlots;
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

        traitSlots = GetComponentsInChildren<PlayerTraitSlots>();
        foreach (PlayerTraitSlots ts in traitSlots)
        {
            foreach (GameObject t in allTraitObjects)
            {
                ts.SpawnTrait(t);
            }
        }

        foreach (PlayerTraitSlots ts in traitSlots)
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
        TraitPoints += tp;
    }

    public void SwapTrait(int traitSlotIndex, GameObject trait) {
        traitSlots[traitSlotIndex].SwapTrait(trait);
    }

    public void ActivateEveryAttack() {
        foreach (var aT in AttackTraits)
        {
            aT.ActivateAttack();
        }
    }
}
