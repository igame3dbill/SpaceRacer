using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (!GameManager.INSTANCE.GuiManager.HasOpenGui && Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.INSTANCE.GuiManager.OpenGui(pausePanel);
        }
    }
}