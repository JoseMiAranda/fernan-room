using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodScanner : MonoBehaviour
{
    List<string> expectedIngredients = new() { "knife", "mushroom", "fish" };
    List<ObjectGrabbable> ingredients = new();
    Collider sphereCollider;

    public float radius = 5f;
    public float explosionForce = 70f;

    private void Awake()
    {
        sphereCollider = GetComponent<Collider>();
        Debug.Log(sphereCollider == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<ObjectGrabbable>() != null)
        {
            if (!ingredients.Contains(other.gameObject.GetComponent<ObjectGrabbable>())) // Avoid double enter
            {
                ingredients.Add(other.gameObject.GetComponent<ObjectGrabbable>());
                CheckIngredients();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ingredients.Contains(other.gameObject.GetComponent<ObjectGrabbable>())) // Avoid double exit
        {
            ingredients.Remove(other.gameObject.GetComponent<ObjectGrabbable>());
        }
    }

    private void CheckIngredients()
    {
        if(ingredients.Count < 3)
        {
            return;
        }
        if(!expectedIngredients.All(ingredient => ingredients.Any(readedIngredient => readedIngredient.value == ingredient))) // Checks ingredients are correct
        {
            Explode();
        }
        else
        {
            // Round 4
        }
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce * 10, transform.position, radius);
            }
        }
    }
}
