using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodScanner : MonoBehaviour
{
    List<string> expectedIngredients = new();
    List<ObjectGrabbable> ingredients = new();
    Collider sphereCollider;

    public ParticleSystem explosionEffect;
    public ParticleSystem particles;
    public float radius = 5f;
    public float explosionForce = 70f;

    public void Constrcutor(List<string> ingredients)
    {
        this.expectedIngredients = ingredients;
    }

    private void Awake()
    {
        sphereCollider = GetComponent<Collider>();
        Debug.Log(sphereCollider == null);
        if (particles != null)
        {
            particles = Instantiate(particles, transform.position, transform.rotation);
            particles.transform.parent = transform;
            particles.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); // Fixing particles size
        }
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
        if (GameManager.Instance.getRound() == 3)
        {
            if (ingredients.Contains(other.gameObject.GetComponent<ObjectGrabbable>())) // Avoid double exit
            {
                ingredients.Remove(other.gameObject.GetComponent<ObjectGrabbable>());
            }
        }
    }

    private void CheckIngredients()
    {
        if (ingredients.Count < 3)
        {
            return;
        }
        if (!expectedIngredients.All(ingredient => ingredients.Any(readedIngredient => readedIngredient.value == ingredient))) // Checks ingredients are correct
        {
            //GameManager.Instance.ShowWarning();
            Explode(true);
        }
        else
        {
            GameManager.Instance.NextRound();
        }
    }

    public void Explode(bool explosion)
    {
        if (explosion)
        {
            Destroy(Instantiate(explosionEffect, transform.position, transform.rotation), 3);
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce * 10, transform.position, radius);
            }
        }
        AudioManager.Instance.PlaySfx(Sfxs.explosion);
    }

    public List<ObjectGrabbable> Ingredients()
    {
        return ingredients;
    }

    public Collider Collider()
    {
        return sphereCollider;
    }
}
