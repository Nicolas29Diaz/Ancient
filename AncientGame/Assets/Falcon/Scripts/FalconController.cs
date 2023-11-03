using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalconController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("References")]
    public Camera playerCamera;
    [SerializeField] AudioSource footStepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;


    [Header("HabilityToggle")]
    [SerializeField] private bool headBobHability = true;
    [SerializeField] private bool footSoundHability = true;


    [Header("General")]
    public float gravity = -9.81f;

    [Header("Movement")]
    public float flySpeed = 3f;
    public float moveUp = 1f;
    public float moveDown = -1f;
    public float verticalVelocity = 0f;

    [Header("Stamina")]
    public float actualStamina;
    public float velRegainStamina = 1f;
    public float maxStamina = 50f;


    [Header("Rotation")]
    public float rotationSensitivity = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationInput = Vector3.zero;
    private float cameraVerticalAngle;

    [Header("Teclas")]
    public KeyCode crouchKey = KeyCode.Q;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;

    [Header("HeadBobing General")]
    [SerializeField] private float bobSpeed = 10f;

    [Header("HeadBobing X")]
    [SerializeField] private float flyBobAmplitudeX = 0.05f;
    [SerializeField, Range(0, 5)] private float flyFrequencyBobX = 1.0f;

    [SerializeField] private float iddleBobAmplitudeX = 0.05f;
    [SerializeField, Range(0, 5)] private float iddleFrequencyBobX = 1.0f;

    [Header("HeadBobing Y")]
    [SerializeField] private float flyBobAmplitudeY = 0.05f;
    [SerializeField, Range(0, 5)] private float flyFrequencyBobY = 1.0f;

    [SerializeField] private float iddleBobAmplitudeY = 0.05f;
    [SerializeField, Range(0, 5)] private float iddleFrequencyBobY = 1.0f;





    private bool isFlying = false;


    private float defauktYPos = 0;
    private float defauktXPos = 0;
    private float timerBob = 0;


    [Header("Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    private float footStepTimer = 0;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defauktYPos = playerCamera.transform.localPosition.y;
        defauktXPos = playerCamera.transform.localPosition.x;
        actualStamina = maxStamina;
    }



    private void Update()
    {


        HandleMovement();
        HandleRotation();
        HandleFly();
        //UpCollision();
        //HandleCrouching();
        //HandleStamina();
        if (headBobHability)
        {
            HandleHeadBobing();
        }
        //if (footSoundHability)
        //{
        //    HandleFlySound();
        //}


    }

    private void HandleFlySound()
    {
    

        footStepTimer -= Time.deltaTime;

        if (footStepTimer <= 0)
        {
            // Dibuja el raycast en la escena
            footStepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)]);
 
            footStepTimer = baseStepSpeed;    
        }

    }


    private void HandleMovement()
    {
   
        //currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.TransformDirection(new Vector3(horizontalInput/3, verticalVelocity, (verticalInput > 0.1f ? verticalInput: verticalInput/3)) * flySpeed);

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleFly()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            verticalVelocity = moveUp;
        }
        else if(Input.GetKey(KeyCode.LeftShift))
        {
            verticalVelocity = moveDown;
        }
        else
        {
                verticalVelocity = 0f;
        }

    
    }

    private void HandleStamina()
    {

       ConsumeStamina();
      
    }
    private void ConsumeStamina()
    {
        if (actualStamina > 0)
        {
            actualStamina -= velRegainStamina * Time.deltaTime;
        }
        else
        {
            //canRun = false;
        }

    }
    private void RegainStamina()
    {
        if (actualStamina <= maxStamina)
        {
            actualStamina += velRegainStamina * Time.deltaTime;
        }
        else
        {
            //canRun = true;
        }

    }

    private void HandleRotation()
    {

        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensitivity;
        rotationInput.y = -Input.GetAxis("Mouse Y") * rotationSensitivity;

        // Gira el personaje horizontalmente
        transform.Rotate(Vector3.up * rotationInput.x);

        // Gira la cámara verticalmente
        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

    }


    private void HandleHeadBobing()
    {
        if ((moveDirection.x > 0.1f && Mathf.Abs(moveDirection.z) > 0.1f) || moveDirection.x > 0.1f)
        {
            isFlying = true;
           
        }
        else
        {
            isFlying = false;
        }

        timerBob += Time.deltaTime * (bobSpeed);

        playerCamera.transform.localPosition = new Vector3(
         defauktXPos + Mathf.Sin(timerBob * (isFlying ? flyFrequencyBobX : iddleFrequencyBobX)) * (isFlying ? flyBobAmplitudeX : iddleBobAmplitudeX),
         defauktYPos + Mathf.Sin(timerBob * (isFlying ? flyFrequencyBobY: iddleFrequencyBobY)) * (isFlying ? flyBobAmplitudeY : iddleBobAmplitudeY),
         playerCamera.transform.localPosition.z
        );

    }

    //defauktXPos + MathF.Sin(timerBob* frequencyBob/2) * (crouching? crouchBobAmplitude : running? sprintBobAmplitude : walkBobAmplitude),
}
