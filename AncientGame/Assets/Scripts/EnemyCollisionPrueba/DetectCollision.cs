using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    // Velocidad de rotación del enemigo
    public float velocidadRotacion = 5f;

    private bool girandoHaciaImpacto = false;
    private Quaternion rotacionDeseada;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si la colisión involucra al objeto del jugador (ajusta según tu juego)
        if (collision.gameObject.CompareTag("Stone"))
        {
            // Obtiene la posición del objeto que colisionó
            Vector3 posicionImpacto = collision.contacts[0].point;

            // Calcula la dirección hacia la posición de impacto
            Vector3 direccionImpacto = posicionImpacto - transform.position;
            direccionImpacto.y = 0f; // Asegura que la rotación sea horizontal

            // Calcula la rotación necesaria para mirar hacia la posición de impacto
            rotacionDeseada = Quaternion.LookRotation(direccionImpacto);

            // Activa la bandera para comenzar a girar gradualmente
            girandoHaciaImpacto = true;
        }
    }

    private void Update()
    {
        if (girandoHaciaImpacto)
        {
            // Gira gradualmente hacia la dirección de impacto
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);

            // Si estamos cerca de la rotación deseada, detenemos la rotación gradual
            if (Quaternion.Angle(transform.rotation, rotacionDeseada) < 1.0f)
            {
                girandoHaciaImpacto = false;
            }
        }
    }
}
