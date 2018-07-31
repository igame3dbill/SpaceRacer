using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKey)
        {
            Debug.Log("Works");
        }
        else
        {
            Debug.Log("Doesn't Work");
        }
    }
}
