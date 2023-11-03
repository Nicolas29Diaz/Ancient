using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToPlayer : MonoBehaviour
{
    public GameObject falconObj, playerObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            falconObj.SetActive(false);
            playerObj.SetActive(true);
        }
    }
}
