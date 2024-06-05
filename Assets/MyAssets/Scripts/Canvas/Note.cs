using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Canvas canvas;
    private TextMeshProUGUI noteText;

    private void Awake()
    {
        canvas.gameObject.SetActive(false);
        noteText = canvas.transform.Find("NoteImage").GetChild(0).GetComponent<TextMeshProUGUI>(); // get NoteText
    }

    public void Read(string text, float size)
    {
        noteText.fontSize = size;
        noteText.text = text;
        canvas.gameObject.SetActive(true); // Makes canvas visible
        GameManager.Instance.ResolvePuzzle();
    }

    public void UnRead() {
        canvas.gameObject.SetActive(false); // Makes canvas invisible
        if (GameManager.Instance.getRound() == 0) // Check if we are in introduction
        {
            GameManager.Instance.NextRound(); // Round 1
        }
    }
}
