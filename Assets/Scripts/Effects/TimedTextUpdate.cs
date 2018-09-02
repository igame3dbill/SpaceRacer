using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedTextUpdate : MonoBehaviour {
    [SerializeField]
    float changeRate;
    [SerializeField]
    Text textObject;
    [Multiline]
    public string[] updateText;

    float  currentTime;
    int currentText;
	// Use this for initialization
	void Start () {
        currentTime = 0;
        currentText = 0;
        textObject.GetComponent<Text>().text = updateText[currentText];

    }
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + Time.deltaTime; 
        if(currentTime >=  changeRate)
        {
           
            currentTime = 0;
            currentText++;
            if(currentText > updateText.Length -1)
            { currentText = 0; }

            textObject.GetComponent<Text>().text = updateText[currentText];
        }
	}
}
