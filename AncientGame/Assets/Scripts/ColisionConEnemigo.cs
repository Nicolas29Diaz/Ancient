using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionConEnemigo : MonoBehaviour
{
    public Animator animator; // Asigna el componente Animator del objeto
    public MonoBehaviour scriptToDisable; // Asigna el script que quieres desactivar
    public AudioSource cameraAudioSource;
    public AudioClip hitSound;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // Asegúrate de que el objeto con el que colisionas tiene este tag
        {
            animator.SetBool("muelto", true); // Cambia 'isActive' a true
            scriptToDisable.enabled = false; // Desactiva el script
            cameraAudioSource.PlayOneShot(hitSound);
            Invoke("resetearEscena", 2f);
        }
    }

    public void resetearEscena()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
