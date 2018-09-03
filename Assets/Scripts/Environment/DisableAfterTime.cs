using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour {
    [SerializeField]
    float timeToDisable;
    float currentTime;
	// Use this for initialization
	void Start () {
		currentTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + Time.deltaTime;
        if (currentTime >= timeToDisable)
            this.gameObject.SetActive(false);
	}
}
