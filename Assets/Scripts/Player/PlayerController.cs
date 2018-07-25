using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 1;
    int hConstraint = 2;
    float invulTime = 1;       //invulnerability - measured in seconds
    float invulTimer = 0;
    bool hit = false;
    bool active = true;

    Animator animator;
    Rigidbody2D rigidbody;
    AudioSource audioSource;
    SpriteRenderer sprite;

    [SerializeField] Collider2D noseCollider;

    public int health = 10;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        invulTimer -= Time.deltaTime;

        Vector3 pos = transform.position;
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.Space)) health = 0; //kill button
            if (pos.x < -hConstraint)
                pos.x = -hConstraint;
            else if (pos.x > hConstraint)
                pos.x = hConstraint;
            transform.position = pos;
            animator.SetInteger("AnimState", 0);

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxis("Vertical");

            //Debug.Log(h);
            if (h < 0f)
            {
                animator.SetInteger("AnimState", 1);
            }
            else if (h > 0f)
            {
                animator.SetInteger("AnimState", 2);
            }


            Vector3 rot = transform.eulerAngles;
            rot.z = 0;
            transform.eulerAngles = rot;
            //Debug.Log(h);

            Vector3 velocity = Vector3.up;
            velocity.x = h;
            //velocity += transform.up;
            if (v < 0)
                rigidbody.velocity = velocity.normalized * speed * 0.5f;
            else
                rigidbody.velocity = velocity.normalized * speed;
            if (invulTimer <= 0) hit = false;

        }
        else
        {
            sprite.enabled = false;
        }

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Crashed");
        if (!collision.gameObject.CompareTag("PowerUp") && !hit)
        {
            // Debug.Log(collision.rigidbody.gameObject.name);
            if (collision.rigidbody != null)
            {
                if (transform.position.x > collision.transform.position.x)
                    collision.rigidbody.velocity += new Vector2(-2 / collision.rigidbody.mass, speed);
                else collision.rigidbody.velocity += new Vector2(2 / collision.rigidbody.mass, speed);
            }

            // Check which hitbox is being hit then damage ship
            float damageScale = collision.otherCollider == noseCollider ? 1f : 0.5f;
            TakeDamage(collision.gameObject.GetComponent<Obstacle>().damage * damageScale);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle Collision");
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if(obstacle != null)
        {
            TakeDamage(obstacle.damage);
        }
    }
    public void TakeDamage(float damage)
    {
        if (!hit)
        {
            hit = true;
            invulTimer = invulTime;
            health -= Mathf.CeilToInt(damage);
            audioSource.Play();
            if (health <= 0)
            {
                GameManager.INSTANCE.GameOver(this);
            }
        }
    }
}