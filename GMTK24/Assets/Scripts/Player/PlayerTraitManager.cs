using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraitManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTraitObjects;

    PlayerTraitSlots[] traitSlots;
    List<Trait> activeTraits;
    List<AttackTrait> attackTraits;
    List<MovementTrait> movementTraits;
    List<EatTrait> eatTraits;

    private int traitPoints;
    // Start is called before the first frame update
    void Awake()
    {
        activeTraits = new();
        attackTraits= new();
        movementTraits= new();
        eatTraits= new();

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
                activeTraits.Add(trait);
                ts.EnableCurrentTrait();
            }         
        }

        foreach (Trait t in activeTraits)
        {
            if (t is AttackTrait) {
                attackTraits.Add(t as AttackTrait);
            }
            if (t is MovementTrait) {
                movementTraits.Add(t as MovementTrait);
            }
            if (t is EatTrait) {
                eatTraits.Add(t as EatTrait);
            }
        }
    }

    void Start() { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainTraitPoints(int tp) {
        traitPoints += tp;
    }
}
