using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{

    public float minRelativeVelocity = 2f;  // La velocidad mínima para el volumen mínimo
    public float maxRelativeVelocity = 20f; // La velocidad máxima para el volumen máximo
    public AudioSource audioSource;
    public AudioClip[] clips;

    private void OnCollisionEnter(Collision collision)
    {   
        
        float volume = Mathf.InverseLerp(minRelativeVelocity, maxRelativeVelocity, collision.relativeVelocity.magnitude);

        audioSource.volume = volume;

        audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length - 1)]);

    }

}
