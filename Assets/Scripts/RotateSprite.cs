using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour {


	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * -.005f);
    }
}
