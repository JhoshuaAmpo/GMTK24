using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHP;
    private float curHP;

    ObjectPooler foodPool;

    private void Awake() {
        // need to change below to spawn food from a pool instead of it being dependent on the enemy
       foodPool = GameObject.FindWithTag("FoodPool").GetComponent<ObjectPooler>();

        curHP = maxHP;
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
        GameObject food = foodPool.GetPooledObject();
        if (food) {
            food.transform.position = transform.position;
            food.GetComponent<Food>().SetTraitPoints(1);
            food.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
