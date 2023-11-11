using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToFalcon : MonoBehaviour
{
    public GameObject falconObj, playerObj;
    public float heightSpawn = 10f;
    public bool canChange;
    // Update is called once per frame
    void Update()
    {
        if (canChange)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                falconObj.transform.position = new Vector3(playerObj.transform.position.x, heightSpawn, playerObj.transform.position.z);

                playerObj.SetActive(false);
                falconObj.SetActive(true);


            }
        }
      
    }
}
