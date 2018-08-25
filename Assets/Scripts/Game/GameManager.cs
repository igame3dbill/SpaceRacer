using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE { get; private set; }

    public event Action OnUpdateEvent;

    public TimeSpan time;
    public PlayerController player;
    public GuiManager GuiManager;

	// Use this for initialization
	void Awake () {
        Time.timeScale = 1;
        if (INSTANCE == null)
            INSTANCE = this;
        else
            Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        time += TimeSpan.FromSeconds(Time.deltaTime);
        OnUpdateEvent();
    }

    private void OnDestroy()
    {
        if (INSTANCE == this)
            INSTANCE = null;
    }


}
