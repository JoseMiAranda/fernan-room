using System;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    bool reading = true;
    List<Guid> foundedObjects = new();
    int invisiblObjects = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (reading)
        {
            ProximityDetector proximityDetector = collision.gameObject.GetComponent<ProximityDetector>();
            if (proximityDetector != null)
            {
                Guid guid = proximityDetector.GetGuid();
                bool isValid = true;
                foreach (var foundedGuid in foundedObjects)
                {
                    if (foundedGuid.Equals(guid))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    collision.gameObject.SetActive(false);
                    foundedObjects.Add(guid);
                    Debug.Log(foundedObjects.Count + "/" + invisiblObjects);
                    if (foundedObjects.Count == invisiblObjects)
                    {
                        reading = false;
                        GameManager.Instance.NextRound();
                    }
                }
            }
        }
    }

    internal void TotalInvisibleObjects(int number)
    {
        invisiblObjects = number;
    }
}
