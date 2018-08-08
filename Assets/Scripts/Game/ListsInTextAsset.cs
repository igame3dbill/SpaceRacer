using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using UnityEngine.UI;
[RequireComponent(typeof(TextMesh))]

[ExecuteInEditMode]
public class ListsInTextAsset : MonoBehaviour
{
    // this will read line feed delimited text into a text asset game object
    public bool Activate;
    public int showText = 0;
    public TextAsset TextFile;
    private string theWholeFileAsOneLongString;
    public List<string> item;
    private string[] tableInput;
    private bool hasInit;

    [Multiline]
    public string editingText, gameText;
    public bool editing = true;

    private List<string> TextAssetToList(TextAsset ta)
    {
        return new List<string>(ta.text.Split('\n'));
    }

    public void Init()
    {
        if (TextFile != null)
        {
            //get the whole file
            item = TextAssetToList(TextFile);
            //theWholeFileAsOneLongString = TextFile.text;
            //get the table name
            this.gameObject.name = TextFile.name;
            if (item.Count >= 1)
            {
                showText = 0;
                gameText = item[showText];
                editingText = gameText;

                hasInit = true;
                int kWords = item.Count;
            }

            // Debug.Log(items[kWords - 1]);
        }
    }
    // only works in editor
    /*   void ListFolder() {
           string currentFile = AssetDatabase.GetAssetPath(TextFile);           
           string currentFolder = Path.GetDirectoryName(currentFile);
           DirectoryInfo dir = new DirectoryInfo(currentFolder);
          // Debug.Log(dir);
           FileInfo[] info = dir.GetFiles("*.*");
           foreach (FileInfo f in info)
           {
           }
       }*/

    void TextMeshUpdate()
    {
        TextMesh tTextmesh = this.GetComponent<TextMesh>();
        if (!tTextmesh) { this.gameObject.AddComponent<TextMesh>(); }
        tTextmesh.alignment = TextAlignment.Center;
        if (tTextmesh == null) { return; }

        if (Application.isPlaying)
        {
            tTextmesh.text = gameText;
        }
        else
        {
            tTextmesh.text = editingText;
        }
    }
    // Use this for initialization
    void Start()
    {
        editing = false;
        Init();
        TextMeshUpdate();
    }

    void Awake()
    {
        //Init();
    }

    void incrementShowText(int showText)
    {
        if (showText > item.Count - 1 || showText <= -1)
        {
            showText = 0;
        }
        else
        {
            showText++;
        }
    }
    void Update()
    {
        if (Activate && !hasInit && !editing)
        {
            Init();
            TextMeshUpdate();
            Activate = false;
            hasInit = true;
        }

        if (hasInit && !editing)
        {
            gameText = theWholeFileAsOneLongString;
            editingText = gameText;
            TextMeshUpdate();
        }

    }

}