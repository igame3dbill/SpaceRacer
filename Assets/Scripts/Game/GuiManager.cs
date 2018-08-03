using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), string.Format("time: {0}:{1}:{2} // health: {3}", GameManager.INSTANCE.time.Minutes, GameManager.INSTANCE.time.Seconds, GameManager.INSTANCE.time.Milliseconds, GameManager.INSTANCE.player.health));
    }
}
