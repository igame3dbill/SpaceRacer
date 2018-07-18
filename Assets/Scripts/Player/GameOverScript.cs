using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100, 20), "Game Over");
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 10, 200, 20), "Try Again"))
            SceneManager.LoadScene(0);

    }
}
