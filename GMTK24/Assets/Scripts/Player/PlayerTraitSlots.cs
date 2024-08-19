using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTraitSlots : MonoBehaviour
{
    [SerializeField]
    private GameObject currentTraitObject;
    List<GameObject> allTraits;

    private void Awake() {
        allTraits = new();
    }

    private void Start() {

    }

    public Trait GetCurrentTrait() {
        if (currentTraitObject == null) { return null; }
        return currentTraitObject.GetComponent<Trait>();
    }

    public void SwapTrait(GameObject NewTrait) {
        
        GameObject foundObj = FindTraitInList(NewTrait);
        if (foundObj == null) {
            Debug.LogError(NewTrait.name + " is not in the list of allTraits");
        } else {
            currentTraitObject.SetActive(false);
            currentTraitObject = foundObj;
            currentTraitObject.SetActive(true);
            Debug.Log("SuccesfullySwapped!");
        }
    }
    public void EnableCurrentTrait() {
        
        GameObject foundObj = FindTraitInList(currentTraitObject);
        if (foundObj == null) { 
            Debug.LogError(name + " tried to enable an unknown trait: " + currentTraitObject.GetComponent<Trait>());
            return;
        } else {
            foundObj.SetActive(true);
        }
    }
    
    public void SpawnTrait(GameObject trait){
        GameObject t = Instantiate(trait, transform);
        t.SetActive(false);
        allTraits.Add(t);
    }

    private GameObject FindTraitInList(GameObject traitToFind) {
        return allTraits.Find(obj => obj.GetComponent<Trait>().GetTraitCode() == traitToFind.GetComponent<Trait>().GetTraitCode());
    }

}
