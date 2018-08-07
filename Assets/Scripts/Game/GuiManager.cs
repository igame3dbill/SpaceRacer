using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager : MonoBehaviour {

    float deltaTime = 0.0f;

    // Use this for initialization
    void Start () {
		
	}
<<<<<<< HEAD
	
	// Update is called once per frame
	void Update () {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }
=======
>>>>>>> 944b26c53ab26cf75752ac966473c8a54048626e

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), string.Format("time: {0}:{1}:{2} // health: {3}", GameManager.INSTANCE.time.Minutes, GameManager.INSTANCE.time.Seconds, GameManager.INSTANCE.time.Milliseconds, GameManager.INSTANCE.player.health));
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);

    }
}
