using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(UnityEngine.UI.Button))]
[RequireComponent(typeof(UnityEngine.UI.Image))]
[ExecuteInEditMode]
public class InventoryButtons : MonoBehaviour {
    Image image;
    Button button1;
	// Use this for initialization
	void Start () {
       // this.gameObject.AddComponent<UnityEngine.UI.Image>();
       image = this.gameObject.GetComponent<Image>();
        image.type = Image.Type.Sliced;
        image.color = new Color(1.0f, 1.0f, 1.0f, 0.1f); // Color.clear;
        image.sprite = Resources.Load<Sprite>("Textures/BackgroundNew");

        button1 = this.gameObject.GetComponent<Button>();
        // add button callback to onclick
        button1.onClick.AddListener(() => buttonCallBack(button1));

    }

    void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == button1)
        {
            //Your code for button 1
            Debug.Log("Clicked: " + button1.name);
        }
    }

    void OnDisable()
    {
        //Un-Register Button Events
        button1.onClick.RemoveAllListeners();
        //button2.onClick.RemoveAllListeners();
        // button3.onClick.RemoveAllListeners();
    }
}
