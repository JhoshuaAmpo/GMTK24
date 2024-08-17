using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTraitManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> allTraitObjects = new();

    List<Trait> traits = new();
    List<AttackTrait> attackTraits= new();
    List<MovementTrait> movementTraits= new();
    List<EatTrait> eatTraits= new();

    private int traitPoints;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponentsInChildren<Trait>(traits);
        foreach (Trait t in traits)
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainTraitPoints(int tp) {
        traitPoints += tp;
    }
}
