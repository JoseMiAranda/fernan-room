using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentController : MonoBehaviour
{
    public bool isTarjet = false;
    public int danceIndex = 0;
    private AnimationClip[] studentAnimationClip;

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
        collider.enabled = false;
        SetAnimation(studentAnimationClip.Length - 1); // Death Animation
        if(isTarjet)
        {
            GameManager.Instance.RoundThree();
        }
    }
}
