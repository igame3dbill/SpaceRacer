using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletAI : MonoBehaviour {

    [SerializeField] float speed = 6;
    [SerializeField] float deathTimer = -1;

    Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rigidbody.velocity = transform.up * speed;
		if(deathTimer > 0)
        {
            deathTimer -= Time.fixedDeltaTime;
            if(deathTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
