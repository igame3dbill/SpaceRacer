using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSound : MonoBehaviour
{
    Renderer renderer;
    public AudioClip powerUpSound;
    AudioSource audioSource;
    Collider2D collider;
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the ship!");
            audioSource.Play();
            collider.enabled = false;
            renderer.enabled = false;
        }
    }
}
