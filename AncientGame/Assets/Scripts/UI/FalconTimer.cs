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

    [SerializeField] private float maxTimer = 10f; // Max time in seconds
    [SerializeField] private float actualTime; // Current time in seconds
    [SerializeField] private bool isPlayerActive = true;


    [SerializeField] private bool isCharged;

    [SerializeField] private float flyHeight = 20f;

    [SerializeField] private float scalerDischarge = 0.1f;

    void Start()
    {
        SetActivePlayer();
        slider.maxValue = maxTimer;
    }

    void Update()
    {
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

    }

    void ExitFalcon()
    {
        player.SetActive(true);
        falcon.SetActive(false);
        isPlayerActive = true;
   ;
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