using System.Collections.Generic;
using UnityEngine;

public class RoundThreeManager : MonoBehaviour, IRoundObserver
{
    public Transform objectRespawnPoint;
    readonly List<string> secretIngredients = new() { "knife", "mushroom", "fish" };
    public GameObject churro;
    public GameObject foodScanner;
    private GameObject foodScannerInstance;

    private void Start()
    {
        RoundManager.Instance.RegisterObserver(this);
    }

    public void OnRoundStarted(int round)
    {
        if (round == 3)
        {
            foodScannerInstance = Instantiate(foodScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
            foodScannerInstance.GetComponent<FoodScanner>().Constrcutor(secretIngredients);
        }
    }

    public void OnRoundCleared(int round)
    {
        if (round == 3 && foodScannerInstance != null)
        {
            PartyChurros();
            Destroy(foodScannerInstance);
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.UnregisterObserver(this);
    }

    private void PartyChurros()
    {
        foodScannerInstance.GetComponent<FoodScanner>().Collider().enabled = false;

        Transform foodScannerTransform = foodScannerInstance.GetComponent<FoodScanner>().transform;

        Vector3 position = foodScannerTransform.position;
        position.y += 0.3f;
        foodScannerTransform.position = position;

        // Disable correct ingredients
        List<ObjectGrabbable> ingredients = foodScannerInstance.GetComponent<FoodScanner>().Ingredients();
        foreach (var ingredient in ingredients)
        {
            ingredient.gameObject.SetActive(false);
        }

        for (int i = 0; i < 11; i++)
        {
            Instantiate(churro, foodScannerTransform.position, foodScannerTransform.rotation);
        }

        foodScannerInstance.GetComponent<FoodScanner>().Explode(false);
    }
}
