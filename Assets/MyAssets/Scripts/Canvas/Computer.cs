using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    public Canvas canvas;
    public int round;
    private TMP_InputField inputField;
    private TextMeshProUGUI error;
    private string keyWord = "Odoo";
 
    private bool isCompleted = false;

    public void Read()
    {
        if(isCompleted == false)
        {
            canvas.gameObject.SetActive(true);
            GameManager.Instance.SetCanMove(false);
            Cursor.lockState = CursorLockMode.Confined;
            TextMeshProUGUI proofText = canvas.transform.Find("Texts").GetChild(0).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI roundText = canvas.transform.Find("Texts").GetChild(1).GetComponent<TextMeshProUGUI>();
            error = canvas.transform.Find("Texts").GetChild(2).GetComponent<TextMeshProUGUI>();
            inputField = canvas.transform.GetChild(2).GetComponent<TMP_InputField>(); 
            inputField.onEndEdit.AddListener(ValidateKeyWord);
            // Change text of canvas
            roundText.text = GameManager.Instance.GetRoundText(round);
            proofText.text = "Hola";
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Unread();
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
        canvas.gameObject.SetActive(false);
        GameManager.Instance.SetCanMove(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
