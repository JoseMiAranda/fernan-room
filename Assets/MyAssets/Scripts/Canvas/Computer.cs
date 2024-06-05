using TMPro;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private Canvas canvas;
    private TMP_InputField inputField;
    private TextMeshProUGUI error;
    private TextMeshProUGUI roundText;
    private string roundProof = "Test";
    private string keyWord = "Odoo";
    private bool isReading = false;
 
    private bool isCompleted = false;

    public void Read()
    {
        //if(isCompleted == false)
        //{

        //}
        isReading = true;
        canvas.gameObject.SetActive(true);
        GameManager.Instance.SetCanMove(false);
        Cursor.lockState = CursorLockMode.Confined;
        TextMeshProUGUI proofText = canvas.transform.Find("Texts").GetChild(0).GetComponent<TextMeshProUGUI>();
        //TextMeshProUGUI roundText = canvas.transform.Find("Texts").GetChild(1).GetComponent<TextMeshProUGUI>();
        error = canvas.transform.Find("Texts").GetChild(2).GetComponent<TextMeshProUGUI>();
        inputField = canvas.transform.GetChild(2).GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(ValidateKeyWord);
        // Change text of canvas
        //roundText.text = roundProof;
        proofText.text = roundProof;
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
            error.gameObject.SetActive(true);
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

    internal void Constructor(Canvas canvas, string roundProof)
    {
        this.canvas = canvas;
        this.roundProof = roundProof;

    }
}
