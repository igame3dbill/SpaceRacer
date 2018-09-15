using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSpawner : MonoBehaviour {
    /// <summary>
    /// The Minimum Distance between spawns
    /// </summary>
    [SerializeField] float minFrequency;
    /// <summary>
    /// The maximum distance between spawns
    /// </summary>
    [SerializeField] float maxFrequency;

    /// <summary>
    /// The amount by which the distance of spawns changes
    /// </summary>
    [SerializeField] float frequencyChange;
    /// <summary>
    /// The distance traveled before the frequency changes
    /// </summary>
    [SerializeField] float levelDistance;

    /// <summary>
    /// The horizontal spawn offset
    /// </summary>
    [SerializeField] float spawnOffsetHorizontal;
    /// <summary>
    /// The vertical spawn offset
    /// </summary>
    [SerializeField] float spawnOffsetVertical;
    /// <summary>
    /// The Z level of the spawns
    /// </summary>
    [SerializeField] float zPlane = -1f;

    [SerializeField] float deathDistance;
    float deathDistanceSqr;

    /// <summary>
    /// List of possible object to spawn
    /// </summary>
    [SerializeField] SpawnableObject[] prefabs;
    /// <summary>
    /// The transform of the player object
    /// </summary>
    [SerializeField] Transform player;

    /// <summary>
    /// The last y of the player
    /// </summary>
    float lastY;

    /// <summary>
    /// The distance traveled from last spawn
    /// </summary>
    [SerializeField] float distance;
    /// <summary>
    /// The distance traveled since traveled from last frequency change
    /// </summary>
    [SerializeField] float traveledDistance;
    /// <summary>
    /// The distance between spawns
    /// </summary>
    float frequency;

    /// <summary>
    /// The total weight to for weighted random
    /// </summary>
    float combinedWeight;

    List<GameObject> spawnedObjects;

	// Use this for initialization
	void Start () {
        frequency = maxFrequency;
        distance = Random.Range(0, maxFrequency);
        spawnedObjects = new List<GameObject>();
        deathDistanceSqr = deathDistance * deathDistance;
        GameManager.INSTANCE.OnUpdateEvent += OnUpdate;

        foreach(SpawnableObject obj in prefabs)
        {
            combinedWeight += obj.weight;
        }
	}
	
	// Update is called once per frame
	void OnUpdate () {
        float change = player.position.y - lastY;
        distance += change;
        traveledDistance += change;
        lastY = player.position.y;

        if(minFrequency < frequency && traveledDistance == levelDistance)
        {
            frequency -= frequencyChange;
            if (frequency < minFrequency)
                frequency = minFrequency;
        }
        if (distance >= frequency)
        {
            if(prefabs != null && prefabs.Length > 0)
            {
                float weight = Random.Range(0, combinedWeight);
                foreach(SpawnableObject obj in prefabs)
                {
                    if(weight < obj.weight)
                    {
                        
                        GameObject go = Instantiate(obj.prefab, new Vector3(Random.Range(player.position.x - spawnOffsetHorizontal, player.position.x+spawnOffsetHorizontal), player.position.y + spawnOffsetVertical, zPlane), Quaternion.identity);
                        float randomSize = Random.Range(obj.minSize, obj.maxSize);
                        go.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                        spawnedObjects.Add(go);
                        break;
                    }
                    weight -= obj.weight;
                }
                
            }
            distance -= frequency;
        }
        while(spawnedObjects.Count > 0 &&
            Vector3.SqrMagnitude(player.position-spawnedObjects[0].transform.position) >= deathDistanceSqr) {
            GameObject go = spawnedObjects[0];
            spawnedObjects.Remove(spawnedObjects[0]);
            Destroy(go);
        }
    }

    void OnDestroy()
    {
        if(GameManager.INSTANCE != null)
        {
            GameManager.INSTANCE.OnUpdateEvent -= OnUpdate;
        }
    }

    [System.Serializable]
    class SpawnableObject
    {
        public GameObject prefab;
        public float weight;
        public float minSize = 1f;
        public float maxSize = 1f;
    }
}
