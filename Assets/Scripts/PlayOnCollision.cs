using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
    public AudioSource bruh;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
            bruh.Play();
    }
}
