using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

//[ExecuteInEditMode]
public class LoadLineFedText : MonoBehaviour
{
    // use to load text from regular text files with each item a different line of the text
    // whole folder will load into different objects with their own item lists

    [SerializeField] int lineSpacing = 24;
    public bool importDirectory = false;
    private string appPath;
    public bool hasInit = false;
     int currentFolderNumber = 0;
   
    public void Init()
    {
        // be sure to name the game object the top path name of your files
        appPath = Application.dataPath + "/Resources/" + this.gameObject.name;
        //Debug.Log(appPath);
        string[] dirs = Directory.GetDirectories(appPath, "*"); // appPath, "*", SearchOption.TopDirectoryOnly);  
        foreach (string dir in dirs)
        {
            string folder = dir.Replace(appPath + "\\", "");
            if (dir != null) {
                currentFolderNumber++;
            ListFoldersAsObjects(dir);
            }
        }
        importDirectory = false;
        // only run  once to import the data
        hasInit = true;
    }

    public void Rename(string newName)
    {
        this.gameObject.name = newName;
    }

    //Apply ListsInTextAsset Script to game object to import the text and to attach Text component
    void AttachTextScripts(GameObject newObject, TextAsset textFile)
    {
        // ListsInTextAsset uses file IO to populate item list with strings.;
        newObject.AddComponent<ListsInTextAsset>();
        newObject.GetComponent<ListsInTextAsset>().TextFile = textFile;
        newObject.GetComponent<ListsInTextAsset>().Init();
        
        // continue if we have items
        if (newObject.GetComponent<ListsInTextAsset>().item != null)
        {
            // Apply List_LFasText script component to this object.
            newObject.AddComponent<List_LFasText>();
            List_LFasText listText = newObject.GetComponent<List_LFasText>();
            if (listText != null)
            {
                // transfer list items from file to self contained list display script
                listText.Init();
                if (listText.item.Count >= 1)
                {
                    // files represented as GameObjects  are tagged as Category
                    newObject.tag = "Category";

                    //   Destroy ListFromTextAssets so that the objects can travel independnet of files original text files.
                    // Enable Edit in Edit Mode to produce files Objects in advance of run time.
                    DestroyImmediate(newObject.GetComponent<ListsInTextAsset>());
                    
                }
            }

        }
    }

    // match rect transforms
    public static void MatchOther( RectTransform rt, RectTransform other)
    {
        Vector2 myPrevPivot = rt.pivot;
        myPrevPivot = other.pivot;
        rt.position = other.position;

        rt.localScale = other.localScale;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, other.rect.height);
        //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
        rt.pivot = myPrevPivot;
    }

    // create an object for each folder, scan the folder for .txt files
    void ListFoldersAsObjects(string currentFolder)
    {
        int top = 0;
       
        RectTransform rectTransform;
        GameObject folderObject = new GameObject();
        folderObject.tag = "Directory";
        folderObject.layer = 5; //UI
        folderObject.AddComponent<CanvasRenderer>(); // like  a panel
        folderObject.AddComponent<RectTransform>();
    
        // use SetParent to avoid UI scaling issues
        folderObject.transform.SetParent(this.transform, false);

        // match the size of the folder to  host objects container size
        MatchOther(folderObject.GetComponent<RectTransform>(), this.GetComponent<RectTransform>());

        // increment each Text object item down the UI
        rectTransform = folderObject.GetComponent<RectTransform>();
        top = (currentFolderNumber * lineSpacing);
        rectTransform.localPosition += Vector3.down * top;
      
        folderObject.name = currentFolder.Replace(appPath + "\\", "");
        // add background image and clickable button action to text
        folderObject.AddComponent<InventoryButtons>();
        DirectoryInfo dir = new DirectoryInfo(currentFolder);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)

        {      // this will read a text file		
            if (!f.Name.Contains(".meta") && f.Name.Contains(".txt"))
            {
                // Create new object for each .txt file
                GameObject newObject = new GameObject();
                //Lua table files from previous version appended _Table to let you know what they had in 'em
                string newName = f.Name.Replace("_Table.txt", "");
                newObject.name = newName;
                newObject.layer = 5; //UI
                newObject.transform.SetParent(folderObject.transform, false);

                

               
                // Load file path into Unity TextAsset
                string shortPath = this.gameObject.name + "/" + folderObject.name + "/" + f.Name;
                shortPath = shortPath.Replace(".txt", "");
                TextAsset textFile = (TextAsset)Resources.Load<TextAsset>(shortPath);
                if (textFile != null)
                {
                    // Attach Additional Scripts to Object
                    AttachTextScripts(newObject, textFile);
                    // Match RectTransform to Parent.
                    MatchOther(newObject.GetComponent<RectTransform>(), folderObject.GetComponent<RectTransform>());
                    rectTransform = newObject.GetComponent<RectTransform>();
                    rectTransform.localPosition += Vector3.forward;
                    //textFile = null;

                }
            }

        }

    }
    void Update()
    {
        if (importDirectory && !hasInit)
        {
            Init();
        }
    }

    

}