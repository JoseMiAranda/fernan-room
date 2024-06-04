using UnityEngine;

public class Container : MonoBehaviour
{
    bool reading = true;
    int foundedObjects = 0;
    int invisiblObjects = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if(reading)
        {
            if (collision.gameObject.GetComponent<ProximityDetector>() != null)
            {
                collision.gameObject.SetActive(false);
                foundedObjects++;
                Debug.Log(foundedObjects + "/" + invisiblObjects);
                if (foundedObjects == invisiblObjects)
                {
                    reading = false;
                }
            }
        }
    }

    internal void TotalInvisibleObjects(int number)
    {
        invisiblObjects = number;
    }
}
