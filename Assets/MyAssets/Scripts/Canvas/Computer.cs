using TMPro;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private Canvas canvas;
    private TMP_InputField inputField;
    private string keyWord = "Test";
    private bool isReading = false;
    private bool isCompleted = false;

    public void Read()
    {
        if(isCompleted == false)
        {
            isReading = true;
            canvas.gameObject.SetActive(true);
            GameManager.Instance.SetCanMove(false);
            Cursor.lockState = CursorLockMode.Confined;
            inputField = canvas.transform.GetChild(1).GetComponent<TMP_InputField>();
            inputField.onEndEdit.AddListener(ValidateKeyWord);

            // Change texts
            inputField.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Contraseña"; // placeholder
        }
    }

    private void Update()
    {
        if(isReading) {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Unread();
            }
        }
    }

    public void ValidateKeyWord(string s)
    {
        
        if(!s.Equals(keyWord))
        {
            // Error
        } else
        {
            Debug.Log("Lo has logrado!!!");
            isCompleted = true;
            Unread();
        }
    }

    internal void Unread()
    {
        isReading = false;
        canvas.gameObject.SetActive(false);
        GameManager.Instance.SetCanMove(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    internal void Constructor(Canvas canvas, string keyWord)
    {
        this.canvas = canvas;
        this.keyWord = keyWord;
    }
}
