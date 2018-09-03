using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    [SerializeField] private GameObject shopSystem;
    // Use this for initialization
    void Start()
    {
        shopSystem.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shop"))
        {
            if (!GameManager.INSTANCE.GuiManager.HasOpenGui)
            {            
             GameManager.INSTANCE.GuiManager.OpenGui(shopSystem);
                collision.gameObject.tag = "Untagged";
                collision.collider.enabled = false;
            }
       
        }
    }
}
