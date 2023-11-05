using UnityEngine;

public class Telekinesis : MonoBehaviour
{

    public GameObject heldObj;
    private Rigidbody heldObjRB;

    public float pickupRange = 5.0f;
    public float thrownormalForce = 15f;
    public float throwForwardForce = 15f;
    public float pickUpForce = 150f;
    public LayerMask layerMask;

    private Vector3 mouseMovementVector;

    private Transform holdArea = null;

    public float scrollSpeed = 1.0f;
    public float rotationSpeed = 30.0f;


    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * pickupRange, Color.red);

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
            RotateObject();
            Move();

            // Scroll del mouse para acercar o alejar el objeto
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            
            if (scroll != 0)
            {
                ScrollObject(scroll);
            }

            // Rotación del objeto mientras lo sujetas
           
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
            holdArea = new GameObject("HoldArea").transform;
            holdArea.position = heldObj.transform.position;
            holdArea.rotation = heldObj.transform.rotation;
            holdArea.parent = transform;

            // Hace que el objeto agarrado sea hijo de holdArea
            heldObj.transform.parent = holdArea;

            heldObj.gameObject.layer = 7; //Pasa a ser grabbed


        }
    }

    void Drop()
    {
        

        heldObj.gameObject.layer = 3; //Pasa a ser grabbable
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

        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickUpForce *Time.deltaTime);
        }

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

    void ScrollObject(float scroll)
    {
        // Calcula la nueva posición deseada
        Vector3 newPosition = holdArea.position + transform.forward * scroll * scrollSpeed;

        // Calcula la distancia actual entre el objeto y el jugador
        float distanceToObject = Vector3.Distance(transform.position, holdArea.position);

        // Limita la distancia del scroll hacia adelante
        if (scroll > 0 && distanceToObject + scroll * scrollSpeed < pickupRange)
        {
            holdArea.position = newPosition;

        }
        // Limita la distancia del scroll hacia atrás
        else if (scroll < 0 && distanceToObject + scroll * scrollSpeed > 1.5f)
        {
            holdArea.position = newPosition;

        }

        // Actualiza la posición de holdArea

    }

    void RotateObject()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        heldObj.transform.Rotate(Vector3.up, rotationAmount);
    }
}
