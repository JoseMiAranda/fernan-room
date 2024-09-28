using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public bool isTarjet = false;
    public int danceIndex = 0;
    private AnimationClip[] studentAnimationClip;
    private List<Sfxs> screams = new() { Sfxs.scream_1, Sfxs.scream_2, Sfxs.scream_3, Sfxs.scream_4 };

    private Animator animator;
    private new Collider collider; 
    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();  
        studentAnimationClip = animator.runtimeAnimatorController.animationClips;
        animator.Play(studentAnimationClip[danceIndex].name);
    }

    public void SetAnimation(int index)
    {
        animator.Play(studentAnimationClip[index].name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() != null)
        {
            collider.enabled = false;
            int randScream = Random.Range(0, screams.Count - 1);
            AudioManager.Instance.PlaySfx(screams[randScream]);
            SetAnimation(studentAnimationClip.Length - 1); // Death Animation
            if(isTarjet)
            {
                GameManager.Instance.NextRound();
            } else
            {
                TextManager.Instance.ShowWarning(2);
            }
        }
    }
}
