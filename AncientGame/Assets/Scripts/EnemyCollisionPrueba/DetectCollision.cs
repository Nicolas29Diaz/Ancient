using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Velocidad de rotaci�n del enemigo
    public float velocidadRotacion = 5f;

    private bool girandoHaciaImpacto = false;
    private Quaternion rotacionDeseada;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisi�n involucra al objeto del jugador (ajusta seg�n tu juego)
        if (collision.gameObject.CompareTag("Stone"))
        {
            // Obtiene la posici�n del objeto que colision�
            Vector3 posicionImpacto = collision.contacts[0].point;

            // Calcula la direcci�n hacia la posici�n de impacto
            Vector3 direccionImpacto = posicionImpacto - transform.position;
            direccionImpacto.y = 0f; // Asegura que la rotaci�n sea horizontal

            // Calcula la rotaci�n necesaria para mirar hacia la posici�n de impacto
            rotacionDeseada = Quaternion.LookRotation(direccionImpacto);

            // Activa la bandera para comenzar a girar gradualmente
            girandoHaciaImpacto = true;
        }
    }

    private void Update()
    {
        if (girandoHaciaImpacto)
        {
            // Gira gradualmente hacia la direcci�n de impacto
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);

            // Si estamos cerca de la rotaci�n deseada, detenemos la rotaci�n gradual
            if (Quaternion.Angle(transform.rotation, rotacionDeseada) < 1.0f)
            {
                girandoHaciaImpacto = false;
            }
        }
    }
}
