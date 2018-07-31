using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
	Scene scene;

	// Use this for initialization
	void Start()
    {
		scene = SceneManager.GetActiveScene();
	}

    public void OnGUI()
    {
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
		GUI.Label(new Rect(Screen.width / 2 - 33, Screen.height / 2 - 15, 200, 20), "Game Over");
        GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 20, 200, 20), "Main Menu");
		if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 45, 200, 20), "Try Again"))
			SceneManager.LoadScene(scene.name);
    }
}