using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionConEnemigo : MonoBehaviour
{
    public Animator animator; // Asigna el componente Animator del objeto
    public ColisionConEnemigo scriptToDisable; // Asigna el script que quieres desactivar
    public MonoBehaviour scriptToDisable2;
    public AudioSource cameraAudioSource;
    public AudioClip hitSound;
    public CharacterController characterController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // Aseg�rate de que el objeto con el que colisionas tiene este tag
        {
            animator.SetBool("muelto", true); // Cambia 'isActive' a true

            scriptToDisable.enabled = false; // Desactiva el script
            scriptToDisable2.enabled = false; // Desactiva el script
            characterController.enabled = false;
            
            cameraAudioSource.PlayOneShot(hitSound);
            Invoke("resetearEscena", 2f);
        }
    }
    public void resetearEscena()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
