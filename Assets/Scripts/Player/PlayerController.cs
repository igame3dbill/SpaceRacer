using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float minSpeed = 2f;
    [SerializeField]
    float maxSpeed = 12f;
    
    [SerializeField]
    int hConstraint = 4;
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
    public AudioClip damageSound;
    public AudioClip shieldSound;
    public ParticleSystem shipDebris;
    public AudioClip powerUpSound;
    public GameObject shipShield;
    public GameObject atmosphereSkim;
    float skimTimer = 0f;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        atmosphereSkim.SetActive(false);
        

    }
	
	// Update is called once per frame
	void Update () {
        invulTimer -= Time.deltaTime;
        if (invulTimer <= 0f)
        {
            shipShield.SetActive(false);
        }

        Vector3 pos = transform.position;
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.E)) health = 0; //kill 
            if (Input.GetKeyDown(KeyCode.C)) health++; //cheat
           /* if (pos.x < -hConstraint)
                pos.x = -hConstraint;
            else if (pos.x > hConstraint)
                pos.x = hConstraint;*/
            transform.position = pos;
            animator.SetInteger("AnimState", 0);

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxis("Vertical");
            
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
            if (v < 0f && speed >= minSpeed)
            {
                speed = speed - .05f;
            }
            else if (v > 0f && speed <= maxSpeed)
            {
                speed = speed + .05f;
            }
                rigidbody.velocity = velocity.normalized * speed;
            if (invulTimer <= 0)
            {
                hit = false;
                Transform explosion = this.gameObject.transform.GetChild(0);
                explosion.gameObject.SetActive(false);
            }

        }
        else
        {
            sprite.enabled = false;      
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.INSTANCE.GuiManager.GameOver();
        }
        if (skimTimer >= 1f)
        {
            atmosphereSkim.SetActive(true);
            --skimTimer;
        }
        else
        {
            atmosphereSkim.SetActive(false);
        }

    }

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Crashed");
        if (!collision.gameObject.CompareTag("PowerUp") && !hit)
        {
            // NOTE: Use On Trigger Enter events and trigger colliders
            if (collision.collider.gameObject.layer == 17)
            {
                skimTimer = 100f * (collision.collider.gameObject.GetComponent<Transform>().localScale.x/2);
                //return;
            }
            
            Debug.Log("Object collission!" + collision.gameObject.name);
            // Check which hitbox is being hit then damage ship
            float damageScale = collision.otherCollider == noseCollider ? 1f : 0.5f;

            //StationShop doesn't damage us
            Obstacle obstacleCheck = collision.gameObject.GetComponent<Obstacle>();
            if (obstacleCheck != null)
                TakeDamage(collision.gameObject.GetComponent<Obstacle>().damage * damageScale);
            if (collision.gameObject.GetComponentInChildren<RandomRotator>())
            {
                collision.gameObject.GetComponentInChildren<RandomRotator>().tumble++;
                collision.gameObject.GetComponentInChildren<RandomRotator>().Tumbler();
            }

            if (collision.gameObject.GetComponent<CircleCollider2D>() && !gameObject.CompareTag("Planet"))
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        else if (!collision.gameObject.CompareTag("PowerUp") && hit)
        {
            AudioSource.PlayClipAtPoint(shieldSound, transform.position, 200f);
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            
            health = health + 1;
            //AudioSource.PlayClipAtPoint(powerUpSound, transform.position, 100f);
           // collision.gameObject.SetActive(false);
           
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
        
        if(!hit)
        {
            hit = true;
            invulTimer = invulTime;
            shipShield.SetActive(true);
            health -= Mathf.CeilToInt(damage);
            AudioSource.PlayClipAtPoint(damageSound, transform.position, 100f);
            Instantiate(shipDebris, transform.position, Quaternion.identity);
            Transform explosion = this.gameObject.transform.GetChild(0);
            explosion.gameObject.SetActive(true);
            if (health <= 0)
            {     
                GameManager.INSTANCE.GuiManager.GameOver();
            }
        }
    }
}