using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoSelfDestruct : MonoBehaviour {

    private ParticleSystem thisParticleSystem;

	// Use this for initialization
	void Start () {
        thisParticleSystem = GetComponent<ParticleSystem>();

        if (!thisParticleSystem.loop)
        {
            Destroy(this.gameObject, thisParticleSystem.duration);
        }
    }
	
}
