using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(UnityEngine.UI.Button))]
[RequireComponent(typeof(UnityEngine.UI.Image))]
[ExecuteInEditMode]
public class ClickableText : MonoBehaviour {
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
        button1.onClick.AddListener(() => buttonCallBack(button1));

        /*this.gameObject.AddComponent<ButtonClickDetector>();
        ButtonClickDetector BCD = this.gameObject.GetComponent<ButtonClickDetector>() as ButtonClickDetector;
        BCD.OnEnable();*/
    }

    void buttonCallBack(Button buttonPressed)
    {
        if (buttonPressed == button1)
        {
            //Your code for button 1
            Debug.Log("Clicked: " + button1.name);
        }
    }
    }
