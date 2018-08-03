using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceduralLoader : MonoBehaviour {

    [SerializeField] float offset;
    [SerializeField] GameObject[] levelBlocks;
    [SerializeField] GameObject[] loadedBlocks;
    Transform player;
	// Use this for initialization
	void Start () {
        levelBlocks = Resources.LoadAll<GameObject>("LevelBlocks");
        loadedBlocks = new GameObject[3];
        player = GameObject.FindGameObjectWithTag("Player").transform;
        loadedBlocks[0] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, offset, 0), Quaternion.identity);
        loadedBlocks[1] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, 0, 0), Quaternion.identity);
        loadedBlocks[2] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, -offset, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		if(player.position.y >= offset)
        {
            Vector3 pos = player.position;
            pos.y -= offset;
            player.position = pos;

            for(int i = 2; i >=0; i--)
            {
                pos = loadedBlocks[i].transform.position;
                pos.y -= offset;
                loadedBlocks[i].transform.position = pos;
            }
            Destroy(loadedBlocks[2]);
            loadedBlocks[2] = loadedBlocks[1];
            loadedBlocks[1] = loadedBlocks[0];
            loadedBlocks[0] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0,offset,0), Quaternion.identity);
        }
	}
}
