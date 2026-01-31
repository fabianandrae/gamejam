using StarterAssets;
using TMPro;
using UnityEngine;

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
        if(_input.interact && !_input.isInDialog && !_input.isUIMode)
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitInfo, 3f))
            {
                if(hitInfo.collider.CompareTag("Grabable"))
                {
                    Debug.Log("Hit Grabable Object");
                    GrabTrash(hitInfo);
                }
                else if(hitInfo.collider.CompareTag("NPC"))
                {
                    Debug.Log("Hit NPC");
                    InteractWithNPC(hitInfo);
                }
            }
            _input.interact = false;
        }
        HandleHoldingObject();

    }

    void HandleHoldingObject()
    {
        if(heldObject != null)
        {
            heldObject.transform.position = playerAnchorPoint.transform.position;
        }
    }

    void GrabTrash(RaycastHit hitInfo)
    {
        if(heldObject == null)
        {
                    heldObject = hitInfo.collider.gameObject;
                    heldObject.transform.SetParent(playerAnchorPoint.transform);
                    heldObject.transform.localPosition = Vector3.zero;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().isTrigger = true;
        } else {
                    heldObject.transform.SetParent(null);
                    heldObject.GetComponent<Rigidbody>().isKinematic = false;
                    heldObject.GetComponent<Collider>().isTrigger = false;
                    heldObject = null;
        }
    }

    void InteractWithNPC(RaycastHit hitInfo)
    {
        var npc = hitInfo.collider.GetComponent<NPCInteraction>();
        if(npc != null)
        {
            DialogManager.Instance.StartDialog(npc);
        }
    }
}
