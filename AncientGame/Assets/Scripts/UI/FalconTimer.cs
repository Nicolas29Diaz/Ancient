using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FalconTimer : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject falcon;
    public Slider slider;
    public GameObject energyObj;
    public ParticleSystem particles;

    [SerializeField] private float maxTimer = 10f; // Max time in seconds
    [SerializeField] private float actualTime; // Current time in seconds
    [SerializeField] private bool isPlayerActive = true;


    [SerializeField] private bool isCharged;

    [SerializeField] private float flyHeight = 20f;

    [SerializeField] private float scalerDischarge = 0.1f;

    void Start()
    {
        SetActivePlayer();
        
        energyObj.SetActive(false);
        particles = energyObj.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        slider.maxValue = maxTimer;

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isPlayerActive && isCharged)
            {
                EnterFalcon();
            }
            else
            {
                ExitFalcon();
            }
        }

        if (isPlayerActive)
        {
            StartCharging();
        }
        else
        {
            StartDischarging();
        }
    }

    void SetActivePlayer()
    {
        player.SetActive(true);
        falcon.SetActive(false);
        isPlayerActive = true;

        actualTime = 0f; // Reset actual time when switching to player
    }

    void EnterFalcon()
    {
        falcon.transform.position = new Vector3(player.transform.position.x, flyHeight, player.transform.position.z);
        player.SetActive(false);
        falcon.SetActive(true); 
        isPlayerActive = false;
        energyObj.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        energyObj.SetActive(true);
        particles.time = 80f;
        particles.Play();
    }   

    void ExitFalcon()
    {
        player.SetActive(true);
        falcon.SetActive(false);
        isPlayerActive = true;

        particles.Stop();
        energyObj.SetActive(false);
    }

    void StartCharging()
    {
        //Debug.Log(actualTime);
        if (actualTime >= maxTimer)
        {
            actualTime = maxTimer;
            isCharged = true;
        }
        else
        {
            isCharged = false;
            actualTime = Mathf.Clamp(actualTime, 0f, maxTimer); // Ensure actualTime is within valid range
            actualTime += Time.deltaTime;
            slider.value = actualTime;
        }
    }

    void StartDischarging()
    {
        isCharged = false;

        if (actualTime <= 0f)
        {
            actualTime = 0f;
            ExitFalcon();
        }
        else
        {
            actualTime = Mathf.Clamp(actualTime, 0f, maxTimer); // Ensure actualTime is within valid range
            actualTime -= Time.deltaTime * scalerDischarge;
            slider.value = actualTime;
        }
    }



}