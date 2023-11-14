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
    public GameObject directionView;

    [Header("Configuración de Giro")]
    public float rotationSpeed = 1f; // Ajusta la velocidad de giro

    private bool triggerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") // Aseg�rate de que el objeto con el que colisionas tiene este tag
        {
            animator.enabled = true;
            animator.SetBool("muelto", true); // Cambia 'isActive' a true

            scriptToDisable.enabled = false; // Desactiva el script
            scriptToDisable2.enabled = false; // Desactiva el script
            characterController.enabled = false;

            cameraAudioSource.PlayOneShot(hitSound);
            Invoke("resetearEscena", 2f);
        }

        if (other.gameObject.tag == "Final")
        {
            animator.enabled = true;
            triggerEntered = true;
            scriptToDisable2.enabled = false;
        }
    }
    public void resetearEscena()
    {
        SceneManager.LoadSceneAsync(1);
    }


    private void OnTriggerStay(Collider other)
    {
        if (triggerEntered && other.gameObject.tag == "Final")
        {
            // Obtén la dirección hacia el objeto directionView
            Vector3 directionToLook = directionView.transform.position - transform.position;
            directionToLook.y = 0f; // Mantén la dirección en el plano horizontal

            // Rota la cámara hacia la dirección especificada
            if (directionToLook != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                if (Vector3.Angle(transform.forward, directionToLook.normalized) < 5f)
                {

                    animator.SetBool("Final", true);
                }
            }
        }
    }
}
