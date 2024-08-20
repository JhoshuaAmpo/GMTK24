using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField]
    GameObject EvoMenu;
    [SerializeField]
    TraitSlotSwapManager traitSlotSwapManager;
    PlayerTraitManager ptm;
    PlayerControls pc;

    // Start is called before the first frame update
    void Awake()
    {
        pc = new();   
        ptm = GetComponentInChildren<PlayerTraitManager>();
        pc.Abilities.ActivateAbility.performed += ActivateAbilities;
        pc.Menu.OpenEvolutionMenu.performed += OpenMenu; 
    }

    void OnEnable() {
        pc.Abilities.Enable();
        pc.Menu.Enable();
    }

    void OnDisable() {
        pc.Abilities.Disable();
        pc.Menu.Disable();
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!EvoMenu.activeInHierarchy) {
            EvoMenu.SetActive(true);
            traitSlotSwapManager.UpdateTotalCostText();
        }
    }

    private void ActivateAbilities(InputAction.CallbackContext context)
    {
        // Debug.Log(context);
        // ptm.ActivateEveryAttack();
    }
}
