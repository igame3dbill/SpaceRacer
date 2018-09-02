using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    GameObject mainMenu;
    bool endGame = false;
    
    public void Start()
    {
       mainMenu = GameObject.FindWithTag("Menu");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayGame()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene("Level1");
    }
    public void ExitGame()
    {
        endGame = true;
        mainMenu.SetActive(false);
        Debug.Log("ExitGame");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    void OnGUI()
    {

        // another code above...
        if (endGame == true)
        {
            bool quitGame = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 200, 200, 20), "Really Quit?");
            bool cancel = GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 180, 200, 20), "Cancel.");
            if (quitGame)
            {
                Application.Quit(); // As far as I know, this only works in the compiled game (.exe)
            }
            if (cancel)
            {
                endGame = false;
                SceneManager.LoadScene("MainMenu");
            }
        }

       

    }
}
