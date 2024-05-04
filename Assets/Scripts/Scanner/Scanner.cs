using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private string findedObj = "Cube";
    private bool readingObject = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!readingObject)
        {
            ObjectGrabbable objectGrabbable = collision.gameObject.GetComponent<ObjectGrabbable>();
        
            if (objectGrabbable != null)
            {
                readingObject = true;
                if (objectGrabbable.value == findedObj)
                {
                    Debug.Log("That's a Cube");
                }
                else
                {
                    Debug.Log("That's not a Cube");
                }
                Timer timer = new(ResetRead, null, 1000, 0);
            }

        }
    }

    private void ResetRead(object state)
    {
        readingObject = false;
    }
}