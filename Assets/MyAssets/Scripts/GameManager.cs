using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool dev = false;

    private bool canMove = true;

    // Managers
    private TextManager textManager;

    // Debubing
    public int round = 0;

    // Respawn Points
    Transform objectRespawnPoint;
    Transform containerRespawnPoint;

    // All needed objects must be referenced in the script inspector. The objects passed are prefabs. If you delete them, you destroy the prefab in your system (loss of data).
    // All prefab objects must have a copy (objectInstance).

    // Round 1
    public GameObject carScanner;
    private GameObject carScannerInstance;

    // Round 2
    public GameObject students;
    public GameObject gun;
    private GameObject gunInstance;

    // Round 3
    List<string> secretIngredients = new() { "knife", "mushroom", "fish" };
    public GameObject churro;
    public GameObject foodScanner;
    private GameObject foodScannerInstance;

    // Round 4
    private readonly List<GameObject> bottleScanners = new List<GameObject>();
    private readonly Dictionary<string, string> teacherBottles = new()
    {
        { "Eladio", "Granade" },
        { "Macarena", "Alcohol" },
        { "Antonio", "Lime & Limon" }
    };

    // Round 5
    private const int totalInvisibleObjects = 3;
    public GameObject invisivilityScanner;
    private GameObject invisivilityScannerInstance;
    public GameObject magicMirror;
    private GameObject magicMirrorInstance;
    private readonly List<GameObject> objectGrabbables = new();
    private readonly List<Material> objectGrabbableMaterials = new();
    private readonly List<Material> customMaterials = new();
    private readonly List<int> changedObjectGrabbables = new();

    // Round 6
    private readonly string keyWord = "dash";
    public GameObject computer;
    private GameObject computerInstance;
    public Canvas computerCanvas;

    // End 
    public GameObject key;
    private GameObject keyInstance;
    public GameObject door;
    private void Awake()
    {
        textManager = new TextManager();

        objectRespawnPoint = GameObject.FindGameObjectWithTag("ScannerRespawn").GetComponent<Transform>();
        containerRespawnPoint = GameObject.FindGameObjectWithTag("ContainerRespawn").GetComponent<Transform>();

        if (Instance == null) // Singleton pattern
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There are one more Game Managers!");
        }
        if (!dev)
        {
            round = 0;
        }
        GoToRound(round);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Use only mouse in game screen
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            // Reset grabbable objects 
            foreach (var i in FindObjectsOfType<ObjectGrabbable>())
            {
                i.resetTransform();
            }
        }
        if (dev)
        {
            if (Input.GetKeyUp(KeyCode.F1))
            {
                NextRound();
            }
        }
    }

    public void NextRound()
    {
        ClearRound(round);
        round++;
        GoToRound(round);
    }

    private void ClearRound(int round)
    {
        switch (round)
        {
            case 1:
                ClearRoundOne();
                break;
            case 2:
                ClearRoundTwo();
                break;
            case 3:
                ClearRoundThree();
                break;
            case 4:
                ClearRoundFour();
                break;
            case 5:
                ClearRoundFive();
                break;
            case 6:
                ClearRoundSix();
                break;
            default:
                Debug.LogWarning("You have no round to clear");
                break;

        }
    }

    // Debuging
    private void GoToRound(int round)
    {
        switch (round)
        {
            case 0:
                Introduction();
                break;
            case 1:
                RoundOne();
                break;
            case 2:
                RoundTwo();
                break;
            case 3:
                RoundThree();
                break;
            case 4:
                RoundFour();
                break;
            case 5:
                RoundFive();
                break;
            case 6:
                RoundSix();
                break;
            case 7:
                End();
                break;
            default:
                this.round = 0; // Prevents errors when extracts game texts
                Introduction();
                break;

        }
    }

    // Rounds 
    public void Introduction()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.introduction);
    }

    public void RoundOne()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_1);
        carScannerInstance = Instantiate(carScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
        carScannerInstance.GetComponent<Scanner>().Constructor("Tezla", NextRound, () => { ShowWarning(); });
    }

    public void RoundTwo()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_2);
        students.SetActive(true);
        gunInstance = Instantiate(gun, objectRespawnPoint.position, objectRespawnPoint.rotation);
    }

    public void RoundThree()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_3);
        foodScannerInstance = Instantiate(foodScanner, objectRespawnPoint.position, objectRespawnPoint.rotation);
        foodScannerInstance.GetComponent<FoodScanner>().Constrcutor(secretIngredients);
    }

    public void RoundFour()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_4);
        int count = 0;
        foreach (var teacherBootle in teacherBottles)
        {
            carScanner.GetComponent<Transform>().localScale = new Vector3(0.1f, 1f, 0.1f); //! Test
            float scannerLenght = carScanner.GetComponent<MeshRenderer>().bounds.size.x;
            Vector3 newPosition = objectRespawnPoint.transform.position + new Vector3(count * (scannerLenght + 0.2f) - 1f, 0, 0); // Position between other bottle scanners
            bottleScanners.Add(Instantiate(carScanner, newPosition, objectRespawnPoint.rotation));
            bottleScanners[count].GetComponent<Scanner>().Constructor(teacherBootle.Value, CheckBootleScanners, CheckBootleScanners);
            count++;
        }
    }

    public void RoundFive()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_5);
        magicMirrorInstance = Instantiate(magicMirror, objectRespawnPoint.position, objectRespawnPoint.rotation);
        invisivilityScannerInstance = Instantiate(invisivilityScanner, containerRespawnPoint.position, containerRespawnPoint.rotation);
        invisivilityScannerInstance.transform.Find("Collider").GetComponent<Container>().TotalInvisibleObjects(totalInvisibleObjects);
        GetGrabableObjects();
        RandomInvisibleObjects();
    }

    public void RoundSix()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.round_6);
        computerInstance = Instantiate(computer, objectRespawnPoint.position, objectRespawnPoint.rotation);
        computerInstance.transform.Find("display").GetComponent<Computer>().Constructor(computerCanvas, keyWord);
    }

    public void End()
    {
        textManager.ShowTextsRound(round);
        AudioManager.Instance.PlayMusic(Musics.the_end);
        keyInstance = Instantiate(key, objectRespawnPoint.position, objectRespawnPoint.rotation);
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private void RandomInvisibleObjects()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        System.Random rnd = new();
        do
        {
            bool isValid = true;
            int rndIndex = rnd.Next(0, objectGrabbables.Count - 1);
            for (int i = 0; i < changedObjectGrabbables.Count; i++)
            {
                if (changedObjectGrabbables[i] == rndIndex)
                {
                    isValid = false;
                    break;
                }
            }
            if (isValid)
            {
                changedObjectGrabbables.Add(rndIndex);
                objectGrabbables[rndIndex].gameObject.layer = 8; // 8 = Proximity, Main camera must have Proximity layer disabled
                objectGrabbables[rndIndex].GetComponent<Renderer>().material = customMaterials[rndIndex];
                ProximityDetector proximityDetector = objectGrabbables[rndIndex].AddComponent<ProximityDetector>();
                proximityDetector.player = player;
            }
            if (changedObjectGrabbables.Count == totalInvisibleObjects)
            {
                break;
            }
        } while (true);
    }

    private void GetGrabableObjects()
    {
        foreach (var objectGrabbable in FindObjectsOfType<ObjectGrabbable>())
        {
            Renderer renderer = objectGrabbable.gameObject.GetComponent<Renderer>();
            if (renderer != null) // Ensures getting objects whose renderer we can extract their materials
            {
                objectGrabbables.Add(objectGrabbable.gameObject);
                Material customMaterial = new Material(Shader.Find("Custom/Proximity"));
                objectGrabbableMaterials.Add(renderer.material);
                customMaterial.SetTexture("_MainTex", renderer.material.mainTexture);
                customMaterials.Add(customMaterial);
            }
        }
    }

    // Clear Rounds. It is used to preparate the scenary to call the next round in order to use debuging sucessfully
    public void ClearRoundOne()
    {
        Destroy(carScannerInstance);
    }

    public void ClearRoundTwo()
    {
        Destroy(students);
        Destroy(gunInstance);
    }

    public void ClearRoundThree()
    {
        PartyChurros();
        Destroy(foodScannerInstance);
    }

    void ClearRoundFour()
    {
        // In progress
        foreach (var bottleScanner in bottleScanners)
        {
            Destroy(bottleScanner);
        }
    }

    public void ClearRoundFive()
    {
        foreach (int index in changedObjectGrabbables)
        {
            objectGrabbables[index].SetActive(true);
            objectGrabbables[index].layer = 0; // 0 = Default
            objectGrabbables[index].GetComponent<Renderer>().material = objectGrabbableMaterials[index];
        }
        Destroy(invisivilityScannerInstance);
        Destroy(magicMirrorInstance);
    }

    public void ClearRoundSix()
    {
        Destroy(computerInstance);
    }

    private void CheckBootleScanners()
    {
        foreach (var bottleScanner in bottleScanners)
        {
            if (!bottleScanner.GetComponent<Scanner>().IsFound())
            {
                return;
            }
        }
        NextRound();
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

    public void ShowWarning(string warning)
    {
        textManager.ShowWarning(warning);
    }

    public void ResolvePuzzle()
    {
        textManager.ResolvePuzzle();
    }

    public string getProof()
    {
        return textManager.Proof(round);
    }

    public float getSize()
    {
        return textManager.Size(round);
    }

    public float getRound()
    {
        return round;
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    void ShowWarning()
    {
        textManager.ShowWarning(round);
    }
}
