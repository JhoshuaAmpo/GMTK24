using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TraitSlotSwapManager : MonoBehaviour
{
    public static TraitSlotSwapManager Instance {get; private set;}

    [Header("Amoeba Buttons")]
    [SerializeField]
    private List<Button> slotButtons;

    [Header("Trait Menu Buttons")]
    [SerializeField]
    private Button cancelButton;
    [SerializeField]
    private Button confirmButton;

    [SerializeField]
    private List<Button> traitButtons;

    [Header("Trait Cost Section")]
    [SerializeField]
    TextMeshProUGUI totalCostText;
    private int totalTraitCost = 0;

    [Space]
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
        confirmButton.onClick.AddListener(delegate{SlotButtonsInteractable(true);});
        foreach (var b in slotButtons)
        {
            b.onClick.AddListener(delegate{TraitSlotButtonClicked(b);});
        }
        foreach (var b in traitButtons)
        {
            b.onClick.AddListener(delegate{SwapTrait(b);});
        }
    }

    private void Start() {
        UpdateTotalCostText();
        
    }

    private void AbortTraitSwap() {
        SlotButtonsInteractable(true);
    }
    private void TraitSlotButtonClicked(Button b) {
        currentSlotButton = b;
        foreach (var sB in slotButtons)
        {
            sB.interactable = false; 
        }
    }

    private void SwapTrait(Button b) {
        // TraitButtonsInteractable(true);
        // b.interactable = false;
        Trait trait = b.GetComponentInChildren<Trait>();
        if (trait.tCost + playerTraitManager.TraitPoints > playerTraitManager.GetTotalTraitCost()) {
            Debug.Log("Can't afford this!");
            return;
        }
        GameObject traitGO = trait.gameObject;
        int traitSlotIndex = slotButtons.IndexOf(currentSlotButton); 
        if (traitSlotIndex == -1) {
            Debug.LogWarning("Can't find " + currentSlotButton + " in slotButtons");
            return;
        }
        playerTraitManager.SwapTrait(traitSlotIndex, traitGO);

        Image newImg = b.transform.Find("Trait Image").GetComponent<Image>();
        currentSlotButton.image.sprite = newImg.sprite;
        currentSlotButton.image.color = newImg.color;
        UpdateTotalCostText();
    }

    private void TraitButtonsInteractable(bool b) {
        foreach (var trait in traitButtons) {
            trait.interactable = b;
        }
    }

    private void SlotButtonsInteractable(bool b) {
        foreach (var sB in slotButtons) {
            sB.interactable = b;
        }
    }

    private void UpdateTotalCostText() {
        totalCostText.text = playerTraitManager.TraitPoints + " / " + playerTraitManager.GetTotalTraitCost();
    }
}
