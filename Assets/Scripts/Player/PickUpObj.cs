using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    [SerializeField] public Transform playerCameraTransform;
    [SerializeField] public Transform objectGrabPointTransform;
    [SerializeField] public GameObject gunGrabPoint;
    [SerializeField] public LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;
    private Gun gun;
    private Note note;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(objectGrabbable == null && note == null) { 
                // Not carring a object or reading, try to grab or read
                float pickUpDistance = 3f;
               if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if(raycastHit.transform.TryGetComponent(out note)) // Read Note
                    {
                        note.Read(GameManager.Instance.getProof(), GameManager.Instance.getSize());
                    } 
                    else if (raycastHit.transform.TryGetComponent(out gun)) // Take Gun
                    {
                        gun.Grab(gunGrabPoint);
                    }
                    else if(raycastHit.transform.TryGetComponent(out objectGrabbable)) // Grab Object
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                if(note != null)
                {
                    note.UnRead();
                    note = null;
                }
                else // Currently carrying something, drop
                {
                    objectGrabbable.Drop();
                    objectGrabbable = null;
                }
            }
        }
    }
}
