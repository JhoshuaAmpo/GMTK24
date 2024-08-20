using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnCooldown = 1f;
    
    [SerializeField]
    [Tooltip("Will spawn objects within this radius")]
    private float spawnOuterRadius;
    [SerializeField]
    [Tooltip("Will NOT spawn objects within this radius")]
    private float spawnInnerRadius;

    [SerializeField]
    private int initialAmountSpawn = 0;

    [SerializeField]
    private ScreenWrap border;

    private float timer = 0;
    private ObjectPooler objectPooler;
    private Transform playerTransform;

    private void Awake() {
        objectPooler = GetComponent<ObjectPooler>();
        List<Transform> tempList = new();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Start() {
        for (int i = 0; i < initialAmountSpawn; i++)
        {
            SpawnObject();
        }
    }

    void Update()
    {
        if (timer <= 0f) {
            SpawnObject();
            timer = spawnCooldown;
        }
        timer -= Time.deltaTime;
    }

    void SpawnObject(){
        GameObject obj = objectPooler.GetPooledObject(); 
        if (obj != null) {
            Vector2 spawnPos;
            do {
                spawnPos = Random.insideUnitCircle.normalized;
            } while ( spawnPos == Vector2.zero);
            spawnPos *= Random.Range(spawnInnerRadius, spawnOuterRadius);
            spawnPos += (Vector2)playerTransform.position;
            obj.transform.position = CheckBorder(spawnPos);
            obj.SetActive(true);
        }
    }

    Vector2 CheckBorder(Vector2 sp) {
        if (sp.x < border.XMin || sp.x > border.XMax) {
            sp.x += 2 * playerTransform.position.x - sp.x;
        }
        if (sp.y < border.YMin || sp.y > border.YMax) {
            sp.y += 2 * playerTransform.position.y - sp.y;
        }
        return sp;
    }
}
