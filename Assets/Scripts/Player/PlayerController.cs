using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 1;
    Rigidbody2D rigidbody;
    AudioSource audioSource;
    [SerializeField]
    int health = 10;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 rot = transform.eulerAngles;
        rot.z = h * -80;
        transform.eulerAngles = rot;
        Vector3 velocity = Vector3.up;
        velocity += transform.up;
        if(v < 0)
            rigidbody.velocity = velocity * speed*0.5f;
        else
            rigidbody.velocity = velocity * speed;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Crashed");
        if (!collision.gameObject.CompareTag("PowerUp"))
        {
            TakeDamage(collision.otherRigidbody.mass);
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
        
        SceneManager.LoadScene(0);
    }
}
