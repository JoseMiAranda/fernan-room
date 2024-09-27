using TMPro;
using UnityEngine;

public class Note : MonoBehaviour, IReadable
{
    public Canvas canvas;
    private TextMeshProUGUI noteText;
    private void Awake()
    {
        canvas.gameObject.SetActive(false);
        noteText = canvas.transform.Find("NoteImage").GetChild(0).GetComponent<TextMeshProUGUI>(); // get NoteText
    }

    public void Read()
    {
        noteText.fontSize = TextManager.Instance.Size(GameManager.Instance.round);
        noteText.text = TextManager.Instance.Proof(GameManager.Instance.round);
        canvas.gameObject.SetActive(true);
    }
    public void UnRead()
    {
        canvas.gameObject.SetActive(false);
        if (GameManager.Instance.round == 0)
        {
            GameManager.Instance.NextRound();
        } else
        {
            TextManager.Instance.ShowGuidance(Guidance.resolve);
        }
    }
}
