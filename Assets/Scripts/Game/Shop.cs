using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private GameObject shopPanel;
    // Use this for initialization
    void Start()
    {
        shopPanel.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            if (!shopPanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if (shopPanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
        else return;
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        shopPanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        shopPanel.SetActive(false);
        //enable the scripts again
    }
}
