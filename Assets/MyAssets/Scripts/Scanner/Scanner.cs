using System;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private string findedObj;
    private bool readingObject = false;
    private bool isFound;

    // Actions == Methods in other programmign languages
    private Action onSuccess;
    private Action onFail;


    public void Constructor(string tag, Action onSuccess, Action onFail) // Whenever you instantiate a object with this script. You MUST call it, in order this works
    {
        findedObj = tag;
        this.onSuccess = onSuccess;
        this.onFail = onFail;
    }

    private void OnCollisionEnter(Collision collision) // Validate the last object
    {
        if(findedObj != null)
        {
            if (!readingObject)
            {
                ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>(); // Scanner only reads ObjectGrabbable objects

                if (objectGrabbable != null)
                {
                    readingObject = true;
                    if (objectGrabbable.value == findedObj)
                    {
                        isFound = true;
                        onSuccess();
                    }
                    else
                    {
                        isFound = false;
                        onFail();
                    }
                    Timer timer = new(ResetRead, null, 1000, 0);
                }

            }
        }
    }

    private void OnCollisionExit(Collision collision) // Validate the last object
    {
        if (findedObj != null)
        {
            if (!readingObject)
            {
                ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>(); // Scanner only reads ObjectGrabbable objects

                if (objectGrabbable != null)
                {
                    
                    if (objectGrabbable.value == findedObj)
                    {
                        isFound = false;
                    }
                }

            }
        }
    }

    private void ResetRead(object state)
    {
        readingObject = false;
    }

    internal bool IsFound()
    {
        return isFound;
    }
}
