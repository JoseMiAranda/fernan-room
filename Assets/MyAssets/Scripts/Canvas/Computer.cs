using TMPro;
using UnityEngine;

public class Computer : MonoBehaviour, IReadable
{
    private Canvas canvas;
    private TMP_InputField inputField;
    private string keyWord = "Test";

    internal void Constructor(Canvas canvas, string keyWord)
    {
        this.canvas = canvas;
        this.keyWord = keyWord;
    }

    private void Update()
    {
        if (canvas.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            UnRead();
        }
    }

    public void Read()
    {
        canvas.gameObject.SetActive(true);
        //GameManager.Instance.SetCanMove(false);
        Cursor.lockState = CursorLockMode.Confined;
        inputField = canvas.transform.GetChild(1).GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(ValidateKeyWord);

        // Change texts
        inputField.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Contrase\u00f1a"; // placeholder
    }

    public void UnRead()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.Instance.SetCanMove(true);
        canvas.gameObject.SetActive(false);
    }

    public void ValidateKeyWord(string s)
    {
        if (!s.Equals(keyWord))
        {
            TextManager.Instance.ShowWarning(6);
        }
        else
        {
            GameManager.Instance.NextRound();
        }
    }
}
