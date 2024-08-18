using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHP;
    private float curHP;

    [Header("Food")]
    [SerializeField]
    private GameObject foodToSpawn;

    private GameObject foodChild;

    private void Awake() {
        // need to change below to spawn food from a pool instead of it being dependent on the enemy
        if (!foodToSpawn) {
            Debug.LogWarning(name + " has no food to spawn");
        }
        if (!foodToSpawn.GetComponent<Food>()) {
            Debug.LogWarning("food gameObject has no Food component");
        }
        Food foodInChild = GetComponentInChildren<Food>();
        if (!foodInChild) {
            foodChild = Instantiate(foodToSpawn, transform);
        } else {
            foodChild = foodInChild.gameObject;
        }

        curHP = maxHP;
        foodChild.SetActive(false);
    }

    public void TakeDamage(float dmg) {
        curHP -= dmg;
        Debug.Log(name + " HP: " + curHP);
        if (curHP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        // Spawn food
        // hide this actor
        foodToSpawn.SetActive(true);
        gameObject.SetActive(false);
    }
}
