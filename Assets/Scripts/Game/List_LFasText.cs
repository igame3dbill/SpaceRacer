using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
//using System.Text;
//using System.IO;
using UnityEngine.UI;
[RequireComponent(typeof(UnityEngine.UI.Text))]

[ExecuteInEditMode]
public class List_LFasText : MonoBehaviour
{
    // use to load text from regular text files with each item a different line of the text
    // whole folder will load into different objects with their own item lists

    private bool editing = true;
    public bool Activate;
    public int showText = 0;
    public float textTimer = 0.0f;
    private float currentTime = 0.0f;
    private bool hasInit;

    [Multiline]
    public string editingText, gameText;
    public List<string> item;

    public void Init()
    {
        var ls = this.gameObject.GetComponent<ListsInTextAsset>();
        if (ls)
        {
            item = new List<string>();
            item = ls.item;          
           // DestroyImmediate(ls);
        }
        Activate = false;
        hasInit = true;
    }


    void incrementShowText(int i)
    {
        if (i > item.Count - 2 || i <= 0)
        {
            showText = 0;
        }
        else
        {
            showText++;
        }
    }

    /*
                // add items to list by spliting string by comma delimiter
                item.AddRange(tableInput[1].Split(","[0]));
    */

    void TextUpdate()
    {
        
        Text tText = this.GetComponent<UnityEngine.UI.Text>();
        if (!tText) { this.gameObject.AddComponent<UnityEngine.UI.Text>(); }
        tText.alignment = TextAnchor.UpperLeft;
        tText.raycastTarget = false;
        if (tText == null) { return; }
        tText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        tText.fontSize = 18;
        if (Application.isPlaying)
        {
            tText.text = gameText;
        }
        else
        {
            tText.text = editingText;
        }
    }
    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        editing = false;
        Init();
        incrementShowText(0);
        TextUpdate();
        
      

    }

    void Awake()
    {
        //Init();
    }



    void Update()
    {
        //hasn't initialized while in play mode
        if (Activate && !hasInit && !editing)
        {
            Init();
            Activate = false;
            hasInit = true;
            TextUpdate();
        }

        //has initilizaed and is play mode
         if (hasInit && !editing)
         {
             if (textTimer > 0.0f)
             {
                 float timedTextPassed = Time.time - currentTime;
                 if (timedTextPassed >= textTimer)
                 {
                     currentTime = Time.time;
                     incrementShowText(showText);
                 }
             }
             if (showText <= item.Count - 1 && showText >= 0)
             {
                 gameText = item[showText];
                 editingText = gameText;
             }
             TextUpdate();
         }
        
    }

   
}