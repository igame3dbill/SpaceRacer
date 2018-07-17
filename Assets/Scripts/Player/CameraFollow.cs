using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
	// Use this for initialization
	void Start () {
        if (target == null)
            this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = target.position.y + offset.y;
        transform.position = pos;
	}
}
