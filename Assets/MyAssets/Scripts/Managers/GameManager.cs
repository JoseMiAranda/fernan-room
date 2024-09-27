using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isDev = false;
    private bool canMove = true;
    public static GameManager Instance;
    public int round = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple GameManagers found!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartRound();
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
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            NextRound();
        }
    }

    public void NextRound()
    {
        RoundManager.Instance.ClearRound(round);
        if (round != 7)
        {
            round++;
            StartRound();
        }
    }

    private void StartRound()
    {
        RoundManager.Instance.StartRound(round);
    }

    public bool CanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void Exit()
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
}
