using System;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour, IRoundScanner
{
    private bool isFound;
    private int count = 0;

    public List<string> Tags { get; set; }
    public Action OnSuccess { get; set; }
    public Action OnFailure { get; set; }

    private void OnCollisionEnter(Collision collision) // Validate the last object
    {
        if (Tags != null)
        {
            ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>(); // Scanner only reads ObjectGrabbable objects

            if (objectGrabbable != null)
            {
                count++;
                if (objectGrabbable.value == Tags?[0])
                {
                    isFound = true;
                    OnSuccess?.Invoke();
                }
                else
                {
                    isFound = false;
                    OnFailure?.Invoke();
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision) // Validate the last object
    {
        if (Tags != null)
        {
            ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>(); // Scanner only reads ObjectGrabbable objects

            if (objectGrabbable != null)
            {
                count--;
                if (objectGrabbable.value == Tags?[0])
                {
                    isFound = false;
                }
            }
        }
    }

    internal bool IsFound()
    {
        return isFound;
    }

    internal int Count()
    {
        return count;
    }
}
