using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitSlotSwapManager : MonoBehaviour
{
    public static TraitSlotSwapManager Instance {get; private set;}

    [SerializeField]
    private List<Button> slotButtons;
    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private List<Button> traitButtons;
    [SerializeField]
    private PlayerTraitManager playerTraitManager;

    private Button currentSlotButton;
    
    

    private void Awake() {
        if (Instance == null) { Instance = this; } 
        else { 
            Debug.LogWarning("Multiple instnaces of " + Instance + " detected!");
            Destroy(this);
        }
        cancelButton.onClick.AddListener(AbortTraitSwap);
        foreach (var b in slotButtons)
        {
            b.onClick.AddListener(delegate{TraitSlotButtonClicked(b);});
        }
        foreach (var b in traitButtons)
        {
            b.onClick.AddListener(delegate{SwapTrait(b);});
        }
    }

    private void AbortTraitSwap() {
        currentSlotButton.interactable = true;
    }
    private void TraitSlotButtonClicked(Button b) {
        currentSlotButton = b;
        currentSlotButton.interactable = false;
    }

    private void SwapTrait(Button b) {
        GameObject traitGO = b.GetComponentInChildren<Trait>().gameObject;
        int traitSlotIndex = slotButtons.IndexOf(currentSlotButton); 
        if (traitSlotIndex == -1) {
            Debug.LogWarning("Can't find " + currentSlotButton + " in slotButtons");
            return;
        }
        playerTraitManager.SwapTrait(traitSlotIndex, traitGO);    
    }

    // public int GetIndexToButton(GameObject g){
    //     int a = slotButtons.IndexOf(g);
    //     if (a == -1) {
    //         Debug.Log(g.name + " is not in the list");
    //     }
    //     return a;
    // }
}
