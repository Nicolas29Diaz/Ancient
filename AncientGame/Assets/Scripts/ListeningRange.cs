using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
// Asegúrate de que hay un Collider isTrigger asignado al objeto donde este script está adjunto.
[RequireComponent(typeof(SphereCollider))]
public class ListeningRange : MonoBehaviour
{
    // Este será el rango de escucha. Asegúrate de que el radio del SphereCollider coincida con este valor.
    public float hearingRange = 10f;
    public Transform possibleTarget;
    public float noiseLevel;
    private void Start()
    {
        // Configura el collider como un trigger y ajusta su radio.
        SphereCollider collider = gameObject.GetComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = hearingRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró en el trigger es una fuente de ruido.
        NoiseSource noiseSource = other.GetComponent<NoiseSource>();
        if (noiseSource != null)
        {
            if(noiseSource.CalculateNoiseLevel() >= noiseLevel)
            {
                possibleTarget = other.gameObject.transform;
                StartCoroutine(ResetPossibleTargetAfterDelay(8f));
            }
        }
    }


    private IEnumerator ResetPossibleTargetAfterDelay(float delay)
    {
        // Espera durante el tiempo de 'delay' especificado.
        yield return new WaitForSeconds(delay);
        // Restablece possibleTarget a su valor por defecto aquí.
        possibleTarget = null; // Suponiendo que el valor por defecto es null.
    }
}
