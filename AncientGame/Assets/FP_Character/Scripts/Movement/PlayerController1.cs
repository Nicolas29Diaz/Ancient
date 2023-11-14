using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController1 : MonoBehaviour
{
    private CharacterController characterController;

    [Header("References")]
    public Camera playerCamera;
    [SerializeField] AudioSource footStepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] woodClips = default;


    [Header("HabilityToggle")]
    [SerializeField] private bool headBobHability = true;
    [SerializeField] private bool footSoundHability = true;
    [SerializeField] private bool jumpHability = true;


    [Header("General")]
    public float gravity = -9.81f;

    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float crouchSpeed = 1.5f;
    public float runCrouchSpeed = 2.5f;

    [Header("Stamina")]
    public float actualStamina;
    public float velRegainStamina = 1f;
    public float maxStamina = 50f;

    [Header("Jump")]
    public float jumpForce = 8f;
    public float jumpCooldown = 1.0f;
    private float verticalVelocity = 0;

    [Header("Rotation")]
    public float rotationSensitivity = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 rotationInput = Vector3.zero;
    private float cameraVerticalAngle;

    [Header("Teclas")]
    public KeyCode crouchKey = KeyCode.Q;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;


    [Header("HeadBobing")]
    [SerializeField] private float walkBobSpeed = 10f;
    [SerializeField] private float walkBobAmplitude = 0.05f;
    [SerializeField] private float sprintBobSpeed = 13f;
    [SerializeField] private float sprintBobAmplitude = 0.1f;
    [SerializeField] private float crouchBobSpeed = 5f;
    [SerializeField] private float crouchBobAmplitude = 0.025f;
    [SerializeField, Range(0, 5)] private float frequencyBob = 1.0f;
    private float defauktYPos = 0;
    private float defauktXPos = 0;
    private float timerBob = 0;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);

    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0f, 0);

    [SerializeField] private float timeToCrouch = 0.5f;
    [SerializeField] private bool duringCrouchAnim;



    [Header("Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;

    private float footStepTimer = 0;
    private float GetCurrentOffset => crouching ? baseStepSpeed * crouchStepMultiplier : running ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;



    private bool readyToJump = true;
    private bool upCollision;
    private bool crouching = false;
    private bool running = false;
    private bool canRun = true;
    private Vector2 currentInput;
    private bool rotationHability = true;


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
        if (Input.GetKeyDown(KeyCode.C))
        {
            rotationHability = !rotationHability;
        }
        
        HandleMovement();
        if (rotationHability){
              HandleRotation();
        }

      
        UpCollision();
        HandleCrouching();
        HandleStamina();
        if (headBobHability)
        {
            HandleHeadBobing();
        }
        if (footSoundHability)
        {
            HandleFootSteps();
        }

        
    }

    private void HandleFootSteps()
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero)
        {
            footStepAudioSource.Stop();
          
        }
        else
        {
         
            footStepTimer -= Time.deltaTime;

            if (footStepTimer <= 0)
            {
             
                // Dibuja el raycast en la escena
               

                if (Physics.Raycast(characterController.transform.position, Vector3.down, out RaycastHit hit, 3))
                {
                  
                    switch (hit.collider.tag)
                    {
                        case "GrassFloor":
                            Debug.Log("SONAR4");
                            footStepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)],0.1f);
                            break;

                        case "WoodFloor":

                            footStepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)],0.2f);
                            break;

                        default:
                            break;
                    }
                }

                footStepTimer = GetCurrentOffset;
               
            }
        }

      

    }


    private float SpeedMovement()
    {
        float speed = walkSpeed;

        if (Input.GetKey(runKey) && canRun)
        {
            if (crouching)
            {
                speed = runCrouchSpeed;
               
            }
            else
            {
                speed = runSpeed;
            }

            running = true;
        }
        else
        {
            if (crouching)
            {
                speed = crouchSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            running = false;
        }

        return speed;
    }

   
    private void HandleMovement()
    {
        float speed = SpeedMovement();


        if (characterController.isGrounded)
        {
            if (jumpHability)
            {
                HandleJump();
            }
            
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        currentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = transform.TransformDirection(new Vector3(horizontalInput, verticalVelocity, verticalInput) * speed);

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && readyToJump)
        {
            readyToJump = false;
            verticalVelocity = jumpForce;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            verticalVelocity = -0.5f;
        }
    }
    private void ResetJump()
    {
        //readyToJump = true;
    }

   

    private void HandleCrouching()
    {
        if (Input.GetKeyDown(crouchKey) && !duringCrouchAnim)
        {
            StartCoroutine(CrouchStand());
        }

    }
    private IEnumerator CrouchStand()
    {
        if (crouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 2f))
        {
            yield break;
        }

        duringCrouchAnim = true;
        float timeElapsed = 0;
        float targetHeight = crouching ? standingHeight : crouchHeight;
        float currentHight = characterController.height;
        Vector3 targetCenter = crouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHight, targetHeight, timeElapsed/timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        crouching = !crouching;
        duringCrouchAnim = false;



    }
   
 

    private void HandleStamina()
    {
        if (running)
        {
            ConsumeStamina();
        }
        else
        {
            RegainStamina();
        }
    }
    private void ConsumeStamina()
    {
        if (actualStamina > 0)
        {
            actualStamina -= velRegainStamina * Time.deltaTime;
        }
        else
        {
            canRun = false;
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
            canRun = true;
        }
        
    }

    private void HandleRotation()
    {
        
            rotationInput.x = Input.GetAxis("Mouse X") * rotationSensitivity;
            rotationInput.y = -Input.GetAxis("Mouse Y") * rotationSensitivity;

            // Gira el personaje horizontalmente
            transform.Rotate(Vector3.up * rotationInput.x);

            // Gira la c�mara verticalmente
            cameraVerticalAngle += rotationInput.y;
            cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);
            playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalAngle, 0f, 0f);

        

    }

    private void UpCollision()
    {
        RaycastHit hit;

        Vector3 upDirection = playerCamera.transform.up; // Obtiene la direcci�n "arriba" de la c�mara

        if (Physics.Raycast(playerCamera.transform.position, upDirection, out hit, 1f))
        {
            upCollision = true;
            readyToJump = false;

        }
        else
        {
            upCollision = false;
            readyToJump = true;
        }
    }


    private void HandleHeadBobing()
    {
        if (!characterController.isGrounded) return;

        if(MathF.Abs(moveDirection.x) > 0.1f || MathF.Abs(moveDirection.z) > 0.1f)
        {
            timerBob += Time.deltaTime * (crouching ? crouchBobSpeed : running ? sprintBobSpeed : walkBobSpeed);

            playerCamera.transform.localPosition = new Vector3(
                defauktXPos + MathF.Sin(timerBob * frequencyBob / 2) * (crouching ? crouchBobAmplitude : running ? sprintBobAmplitude : walkBobAmplitude),
                defauktYPos + MathF.Sin(timerBob * frequencyBob) * (crouching ? crouchBobAmplitude : running ? sprintBobAmplitude: walkBobAmplitude),
                playerCamera.transform.localPosition.z
                );
        } 
    }

    //defauktXPos + MathF.Sin(timerBob* frequencyBob/2) * (crouching? crouchBobAmplitude : running? sprintBobAmplitude : walkBobAmplitude),

}
