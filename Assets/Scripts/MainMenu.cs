using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene("SpriteStaging01");
    }

}
