using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSound : MonoBehaviour
{

    public AudioClip powerUpSound;
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(powerUpSound, collision.gameObject.GetComponent<Transform>().position, 100f);
        }
    }
}
