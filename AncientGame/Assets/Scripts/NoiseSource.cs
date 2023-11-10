using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSource : MonoBehaviour
{
    public float noiseLevelAtMaxSpeed = 1f; // El nivel de ruido cuando el objeto se mueve a su velocidad m�xima.
    public float maxSpeed = 10f; // La velocidad m�xima esperada del objeto.
    public float noiseLevel;

    private Rigidbody _rigidbody;
    private CharacterController _characterController;
    private float _currentSpeed;

    private void Awake()
    {
        // Intenta obtener el componente Rigidbody si est� presente.
        _rigidbody = GetComponent<Rigidbody>();
        // Si no hay Rigidbody, intenta obtener el CharacterController.
        if (_rigidbody == null)
        {
            _characterController = GetComponent<CharacterController>();
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            _currentSpeed = _rigidbody.velocity.magnitude;
        }
        else if (_characterController != null)
        {
            _currentSpeed = _characterController.velocity.magnitude;
        }
        noiseLevel = CalculateNoiseLevel();
    }

    public float CalculateNoiseLevel()
    {
        // Calcula la relaci�n entre la velocidad actual y la velocidad m�xima.
        float speedRatio = _currentSpeed / maxSpeed;
        // Aseg�rate de que la relaci�n no sea mayor que 1.
        speedRatio = Mathf.Clamp01(speedRatio);
        // El nivel de ruido es proporcional a la velocidad actual.
        return speedRatio * noiseLevelAtMaxSpeed;
    }
}
