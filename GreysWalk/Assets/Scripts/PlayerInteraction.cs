using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Other Objects")]
    public Camera playerCamera;
    public GameObject playerAnchorPoint;
    public GameObject heldObject;

    private StarterAssetsInputs _input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input.interact)
        {
            Interact();
            _input.interact = false;
        }
        if(heldObject != null)
        {
            heldObject.transform.position = playerAnchorPoint.transform.position;
        }
    }

    void Interact()
    {
        Debug.Log("Interact pressed");
        if(heldObject == null)
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 3f))
            {
                if(hitInfo.collider.CompareTag("Grabable"))
                {
                    heldObject = hitInfo.collider.gameObject;
                    heldObject.transform.SetParent(playerAnchorPoint.transform);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = false;
                }
            }
        } else
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
