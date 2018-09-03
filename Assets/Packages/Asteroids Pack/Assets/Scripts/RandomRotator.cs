using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    
    public float tumble;

     void Start()
    {
        Tumbler();
    }

    public void Tumbler()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }
}