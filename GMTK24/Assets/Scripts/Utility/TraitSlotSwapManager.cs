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
    private List<Button> confirmButtons;

    [SerializeField]
    private List<Button> traitButtons;

    [Header("Trait Cost Section")]
    [SerializeField]
    TextMeshProUGUI totalCostText;

    [Space]
    [SerializeField]
    private PlayerTraitManager playerTraitManager;

    private List<float> slotButtonsOriginalZRotation; 

    private Button currentSlotButton;
    
    private void Awake() {
        if (Instance == null) { Instance = this; } 
        else { 
            Debug.LogWarning("Multiple instnaces of " + Instance + " detected!");
            Destroy(this);
        }

        cancelButton.onClick.AddListener(AbortTraitSwap);
        foreach (var b in confirmButtons)
        {
            b.onClick.AddListener(delegate{SlotButtonsInteractable(true);});
        }
        foreach (var b in slotButtons)
        {
            b.onClick.AddListener(delegate{TraitSlotButtonClicked(b);});
        }
        foreach (var b in traitButtons)
        {
            b.onClick.AddListener(delegate{SwapTrait(b);});
        }

        slotButtonsOriginalZRotation = new();
        foreach (var b in slotButtons)
        {
            slotButtonsOriginalZRotation.Add(b.image.rectTransform.rotation.eulerAngles.z);
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
        int traitSlotIndex = slotButtons.IndexOf(currentSlotButton); 
        if (traitSlotIndex == -1) {
            Debug.LogWarning("Can't find " + currentSlotButton + " in slotButtons");
            return;
        }
        
        // Check if player can afford trait
        Trait trait = b.GetComponentInChildren<Trait>();
        int thisTraitSlotCost = 0;
        if (playerTraitManager.TraitSlots[traitSlotIndex].GetComponent<Trait>() != null) {
            thisTraitSlotCost = playerTraitManager.TraitSlots[traitSlotIndex].GetComponent<Trait>().tCost;
        }
        if (trait.tCost + playerTraitManager.GetTotalTraitCost() - thisTraitSlotCost >= playerTraitManager.TraitPoints) {
            Debug.Log("Can't afford this!");
            return;
        }

        // Does the actual trait swapping
        GameObject traitGO = trait.gameObject;
        playerTraitManager.SwapTrait(traitSlotIndex, traitGO);
        Image newImg = b.transform.Find("Trait Image").GetComponent<Image>();
        currentSlotButton.image.sprite = newImg.sprite;
        currentSlotButton.image.color = newImg.color;
        // Debug.Log(newImg.rectTransform.rotation.eulerAngles.z);
        currentSlotButton.image.rectTransform.localRotation = Quaternion.Euler(0, 0, slotButtonsOriginalZRotation[slotButtons.IndexOf(currentSlotButton)] + newImg.rectTransform.rotation.eulerAngles.z);
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
        totalCostText.text = playerTraitManager.GetTotalTraitCost() + " / " + playerTraitManager.TraitPoints;
    }
}
