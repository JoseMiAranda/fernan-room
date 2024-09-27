using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    public Transform playerCameraTransform;
    public Transform objectGrabPointTransform;
    public GameObject gunGrabPoint;
    public LayerMask pickUpLayerMask;

    private IInteractable currentInteractable;
    private IReadable currentReadable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable == null && currentReadable == null)
            {
                TryToInteract();
            }
            else
            {
                HandleDropOrUnread();
            }
        }
    }

    private void TryToInteract()
    {
        float pickUpDistance = 3f;
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out IInteractable interactable))
            {
                currentInteractable = interactable;
                if (interactable is ObjectGrabbable)
                {
                    interactable.Interact(objectGrabPointTransform);
                }
                else
                {
                    interactable.Interact(gunGrabPoint.transform); // Gun, Mirror & Key
                }
            }
            else if (raycastHit.transform.TryGetComponent(out IReadable readable)) // Board & Computer
            {
                GameManager.Instance.SetCanMove(false);
                if (readable is Note)
                {
                    currentReadable = readable;
                }
                readable.Read();
            }
        }
    }

    private void HandleDropOrUnread()
    {
        if (currentReadable != null && currentReadable is not Computer)
        {
            GameManager.Instance.SetCanMove(true);
            currentReadable.UnRead();
            currentReadable = null;
        }
        else if (currentInteractable != null)
        {
            if (currentInteractable is ObjectGrabbable grabbable)
            {
                grabbable.Drop();
            }
            currentInteractable = null;
        }
    }
}
