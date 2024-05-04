using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    [SerializeField] public Transform playerCameraTransform;
    [SerializeField] public Transform objectGrabPointTransform;
    [SerializeField] public LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbable;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            if(objectGrabbable == null) { 
                // Not carring a object, try to grab
                float pickUpDistance = 3f;
               if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if(raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // Currently carrying something, drop
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }
    }
}
