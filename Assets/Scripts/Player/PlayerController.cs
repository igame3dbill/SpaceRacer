using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 1;
    Animator animator;
    Rigidbody2D rigidbody;
    AudioSource audioSource;
    [SerializeField]
    int health = 10;
    TimeSpan time;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        time += TimeSpan.FromSeconds(Time.deltaTime);

        Vector3 pos = transform.position;
        if (pos.x < -10)
            pos.x = -10;
        else if (pos.x > 10)
            pos.x = 10;
        transform.position = pos;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 rot = transform.eulerAngles;

        rot.z = h * -30;
        transform.eulerAngles = rot;
        animator.SetFloat("Rotation", h);
        Vector3 velocity = Vector3.up;
        velocity += transform.up;
        if(v < 0)
            rigidbody.velocity = velocity.normalized * speed*0.5f;
        else
            rigidbody.velocity = velocity.normalized * speed;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Crashed");
        if (!collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log(collision.rigidbody.gameObject.name);
            TakeDamage(collision.rigidbody.mass);
        }
    }
    void TakeDamage(float damage)
    {
        health -= Mathf.CeilToInt(damage);
        audioSource.Play();
        if (health <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        this.enabled = false;
        GetComponent<GameOverScript>().enabled = true;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), string.Format("{0}:{1}:{2}", time.Minutes, time.Seconds, time.Milliseconds));
    }
}
