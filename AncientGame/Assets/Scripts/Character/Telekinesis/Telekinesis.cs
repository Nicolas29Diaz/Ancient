using UnityEngine;

public class Telekinesis : MonoBehaviour
{

    public GameObject heldObj;
    private Rigidbody heldObjRB;

    public float pickupRange = 5.0f;
    public float thrownormalForce = 15f;
    public float throwForwardForce = 15f;
    public LayerMask layerMask;

    private Vector3 mouseMovementVector;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
            {
                PickUp(hit.transform.gameObject);
            }
        }
        else if (Input.GetMouseButtonUp(0) && heldObj != null)
        {
            Drop();
        }

        if (heldObj != null)
        {
            Move();
        }


        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            mouseMovementVector = new Vector3(mouseX, mouseY, 0);
        }
    }

    void PickUp(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            heldObj = obj;
            heldObjRB = obj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.drag = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            // Crea holdArea como hijo de la cámara en la posición y rotación inicial del objeto
            Transform holdArea = new GameObject("HoldArea").transform;
            holdArea.position = heldObj.transform.position;
            holdArea.rotation = heldObj.transform.rotation;
            holdArea.parent = transform;

            // Hace que el objeto agarrado sea hijo de holdArea
            heldObj.transform.parent = holdArea;

            heldObj.gameObject.layer = 7;
        }
    }

    void Drop()
    {
        heldObj.gameObject.layer = 3;
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        // Suelta el objeto de holdArea
        heldObj.transform.parent = null;
        heldObj = null;

        // Destruye holdArea
        foreach (Transform child in transform)
        {
            if (child.name == "HoldArea")
            {
                Destroy(child.gameObject);
                break;
            }
        }

        // Aplica la fuerza en la dirección del movimiento del mouse al soltar el objeto
        heldObjRB.AddForce(mouseMovementVector * thrownormalForce, ForceMode.Impulse);

    }

    void Move()
    {
        if (heldObj != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Drop();
                heldObjRB.AddForce(transform.forward * throwForwardForce, ForceMode.Impulse);
            }
            //Vector3 moveDir = (transform.position - heldObj.transform.position);
            //heldObjRB.AddForce(moveDir * pickupForce);
        }
    }


    //public float reachDistance = 5f;
    //public LayerMask interactableLayer;

    //private GameObject grabbedObject;
    //private float initialDistance;
    //private bool isColliding;

    //private ObjectInteractable objectInteractable;
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        TryGrabObject();
    //    }
    //    else if (Input.GetMouseButtonUp(0) && grabbedObject != null)
    //    {
    //        ReleaseObject();
    //    }

    //    if (grabbedObject != null)
    //    {
    //        MoveObject();
    //    }

    //}

    //void TryGrabObject()
    //{
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, reachDistance, interactableLayer))
    //    {
    //        grabbedObject = hit.collider.gameObject;
    //        initialDistance = Vector3.Distance(transform.position, grabbedObject.transform.position);
    //        isColliding = false;

    //        if (hit.collider.GetComponent<ObjectInteractable>() != null)
    //        {
    //            objectInteractable = hit.collider.GetComponent<ObjectInteractable>();
    //            objectInteractable.isColliding = false;
    //        }
    //    }



    //}

    //void ReleaseObject()
    //{
    //    grabbedObject = null;
    //    isColliding = false;
    //}

    //void MoveObject()
    //{
    //    isColliding = objectInteractable.isColliding;
    //    if (grabbedObject != null)
    //    {
    //        // Mantén la distancia inicial desde la cámara.
    //        Vector3 newPosition = transform.position + transform.forward * initialDistance;
    //        grabbedObject.transform.position = newPosition;

    //    
    //    }
    //}



    //public Transform holdArea;
    //private GameObject heldObj;
    //private Rigidbody heldObjRB;

    //public float pickupRange = 5.0f;
    //public float pickupForce = 150f;

    //public LayerMask layerMask;

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
    //        {

    //            PickUp(hit.transform.gameObject);
    //        }

    //    }
    //    else if (Input.GetMouseButtonUp(0) && heldObj != null)
    //    {
    //        Drop();
    //    }

    //    if (heldObj != null)
    //    {
    //        Move();
    //    }

    //}

    //void PickUp(GameObject obj)
    //{
    //    if (obj.GetComponent<Rigidbody>())
    //    {
    //        heldObjRB = obj.GetComponent<Rigidbody>();
    //        heldObjRB.useGravity = false;
    //        heldObjRB.drag = 10;
    //        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

    //        heldObjRB.transform.parent = holdArea;
    //        heldObj = obj;
    //    }
    //}

    //void Drop()
    //{

    //    heldObjRB.useGravity = true;
    //    heldObjRB.drag = 1;
    //    heldObjRB.constraints = RigidbodyConstraints.None;

    //    heldObj.transform.parent = null;
    //    heldObj = null;

    //}

    //void Move()
    //{
    //    if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
    //    {
    //        Vector3 moveDir = (holdArea.position - heldObj.transform.position);
    //        heldObjRB.AddForce(moveDir * pickupForce);
    //    }
    //}





}
