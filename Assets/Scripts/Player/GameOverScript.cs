using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
	Scene scene;
	PlayerController playercontroller;
	GameObject ship;

	// Use this for initialization
	void Start()
    {
		scene = SceneManager.GetActiveScene();
		ship = GameObject.Find("Ship");
	}

    // Update is called once per frame
    void Update()
    {

    }
    public void OnGUI()
    {
		if (ship.GetComponent<PlayerController>().health <= 0)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 20), "Game Over");
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "Try Again"))
				SceneManager.LoadScene(scene.name);
		}
    }
}