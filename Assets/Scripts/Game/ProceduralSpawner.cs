using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour {

    [SerializeField] float minTime;
    [SerializeField] float maxTime;
    [SerializeField] float timeChange;
    [SerializeField] float levelTime;
    [SerializeField] float spawnOffset;

    [SerializeField] SpawnableObject[] prefabs;
    [SerializeField] Transform player;

    float spawnTimer;
    float spawnTime;
    float levelTimer;

    float combinedWeight;

	// Use this for initialization
	void Start () {
        spawnTime = maxTime;
        levelTimer = levelTime;
        GameManager.INSTANCE.OnUpdateEvent += OnUpdate;

        foreach(SpawnableObject obj in prefabs)
        {
            combinedWeight += obj.weight;
        }
	}
	
	// Update is called once per frame
	void OnUpdate () {
        spawnTimer -= Time.deltaTime;
        levelTimer -= Time.deltaTime;

        if(levelTimer <= 0 && spawnTimer > minTime)
        {
            spawnTime -= timeChange;
            if (spawnTime < minTime)
                spawnTime = minTime;
        }
        if (spawnTimer <= 0)
        {
            if(prefabs != null && prefabs.Length > 0)
            {
                float weight = Random.Range(0, combinedWeight);
                foreach(SpawnableObject obj in prefabs)
                {
                    if(weight < obj.weight)
                    {
                        Instantiate(obj.prefab, new Vector3(Random.Range(-spawnOffset, spawnOffset), player.position.y + spawnOffset), Quaternion.identity);
                        break;
                    }
                    weight -= obj.weight;
                }
                
            }
            spawnTimer = spawnTime;
        }
    }
    [System.Serializable]
    class SpawnableObject
    {
        public GameObject prefab;
        public float weight;
    }
}
