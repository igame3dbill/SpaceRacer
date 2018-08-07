using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceduralLoader : MonoBehaviour {

    [SerializeField] bool freeMove;
    [SerializeField] float offset;
    [SerializeField] GameObject[] levelBlocks;
    [SerializeField] GameObject[] loadedBlocks;
    Transform player;
	// Use this for initialization
	void Start () {
        levelBlocks = Resources.LoadAll<GameObject>("LevelBlocks");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (freeMove)
            StartFree();
        else
            StartLinear();
    }
    void StartLinear()
    {
        loadedBlocks = new GameObject[3];
        loadedBlocks[0] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, offset, 0), Quaternion.identity);
        loadedBlocks[1] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, 0, 0), Quaternion.identity);
        loadedBlocks[2] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, -offset, 0), Quaternion.identity);
    }
    void StartFree()
    {
        loadedBlocks = new GameObject[9];
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                loadedBlocks[x * 3 + y] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3((x - 1) * -offset, (y - 1) * -offset, 0), Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (freeMove)
            UpdateFree();
        else
            UpdateLinear();
	}
    void UpdateLinear()
    {
        if (player.position.y >= offset)
        {
            Vector3 pos = player.position;
            pos.y -= offset;
            player.position = pos;

            for (int i = 2; i >= 0; i--)
            {
                pos = loadedBlocks[i].transform.position;
                pos.y -= offset;
                loadedBlocks[i].transform.position = pos;
            }
            Destroy(loadedBlocks[2]);
            loadedBlocks[2] = loadedBlocks[1];
            loadedBlocks[1] = loadedBlocks[0];
            loadedBlocks[0] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, offset, 0), Quaternion.identity);
        }
    }
    void UpdateFree()
    {
        if(player.position.y >= offset)
        {
            Vector3 pos = player.position;
            pos.y -= offset;
            player.position = pos;

            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    pos = loadedBlocks[x * 3 + y].transform.position;
                    pos.y -= offset;
                    loadedBlocks[x * 3 + y].transform.position = pos;
                }
            }
            for (int x = 0; x < 3; x++)
            {
                Destroy(loadedBlocks[x*3+2]);
                loadedBlocks[x*3+2] = loadedBlocks[x*3+1];
                loadedBlocks[x*3+1] = loadedBlocks[x*3];
                loadedBlocks[x*3] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3((x - 1) * -offset, offset, 0), Quaternion.identity);
            }
            /*Destroy(loadedBlocks[2]);
            loadedBlocks[2] = loadedBlocks[1];
            loadedBlocks[1] = loadedBlocks[0];
            loadedBlocks[0] = Instantiate(levelBlocks[Random.Range(0, levelBlocks.Length - 1)], new Vector3(0, offset, 0), Quaternion.identity);*/
        }
        else if(player.position.y < 0)
        {

        }
    }
}
