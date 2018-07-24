using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE { get; private set; }

    public TimeSpan time;
    public PlayerController player;

	// Use this for initialization
	void Start () {
        if (INSTANCE == null)
            INSTANCE = this;
        else
            Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        time += TimeSpan.FromSeconds(Time.deltaTime);
    }

    public void GameOver(PlayerController player)
    {
        GetComponent<GameOverScript>().enabled = true;
        player.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        if (INSTANCE == this)
            INSTANCE = null;
    }
}
