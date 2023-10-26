using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    public bool isColliding;

    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        Debug.Log("Collision en: " + collision.transform.name);
    }


}
