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

    private float timer = 0;
    private ObjectPooler objectPooler;
    private Transform playerTransform;

    private void Awake() {
        objectPooler = GetComponent<ObjectPooler>();
        List<Transform> tempList = new();
        playerTransform = GameObject.FindWithTag("Player").transform;
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
            obj.transform.position = spawnPos + (Vector2)playerTransform.position;
            obj.SetActive(true);
        }
    }
}
