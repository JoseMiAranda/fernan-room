using System;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private string findedObj;
    private bool readingObject = false;
    private bool stopReading = false;

    // Actions == Methods in other languages
    private Action onSuccess;
    private Action onFail;

    private void OnCollisionEnter(Collision collision)
    {
        if(findedObj != null)
        {
            if (!stopReading && !readingObject)
            {
                ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>(); // Scanner only reads ObjectGrabbable objects

                if (objectGrabbable != null)
                {
                    readingObject = true;
                    if (objectGrabbable.value == findedObj)
                    {
                        stopReading = true;
                        onSuccess();
                    }
                    else
                    {
                        onFail();
                    }
                    Timer timer = new(ResetRead, null, 1000, 0);
                }

            }
        }
    }

    public void Constructor(string tag, Action onSuccess, Action onFail) // Whenever you instantiate a object with this script. You MUST call it, in order this works
    {
        findedObj = tag;
        this.onSuccess = onSuccess;
        this.onFail = onFail;
    }

    private void ResetRead(object state)
    {
        readingObject = false;
    }
}
