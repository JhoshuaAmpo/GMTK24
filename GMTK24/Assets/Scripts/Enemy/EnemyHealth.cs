using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float baseMaxHP;
    private float curHP;
    private float calcMaxHP;

    ObjectPooler foodPool;

    private void Awake() {
        // need to change below to spawn food from a pool instead of it being dependent on the enemy
       foodPool = GameObject.FindWithTag("FoodPool").GetComponent<ObjectPooler>();
       calcMaxHP = Mathf.Max(baseMaxHP * transform.localScale.x, baseMaxHP * 2);
       curHP = calcMaxHP;
    }

    private void OnEnable() {
        calcMaxHP = Mathf.Max(baseMaxHP * transform.localScale.x, baseMaxHP * 2);
        curHP = calcMaxHP;
    }

    public void TakeDamage(float dmg) {
        curHP -= dmg;
        Debug.Log(name + " HP: " + curHP);
        if (curHP <= 0) {
            ProcessDeath();
        }
    }

    private void ProcessDeath() {
        GameObject food = foodPool.GetPooledObject();
        if (food) {
            food.transform.position = transform.position;
            float foodScaleMult = Random.Range(0.25f, 5f);
            food.transform.localScale = transform.localScale * foodScaleMult;
            food.GetComponent<Food>().SetTraitPoints((int)calcMaxHP/ 100);
            food.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
