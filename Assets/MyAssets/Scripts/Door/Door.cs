using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Key>() != null)
        {
            GameManager.Instance.QuitGame();
        }
    }
}
