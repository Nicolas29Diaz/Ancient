using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{

    public float minRelativeVelocity = 2f;  // La velocidad m�nima para el volumen m�nimo
    public float maxRelativeVelocity = 20f; // La velocidad m�xima para el volumen m�ximo
    public AudioSource audioSource;
    public AudioClip[] clips;

    private void OnCollisionEnter(Collision collision)
    {   
        
        float volume = Mathf.InverseLerp(minRelativeVelocity, maxRelativeVelocity, collision.relativeVelocity.magnitude);

        audioSource.volume = volume;

        audioSource.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length - 1)]);

    }

}
