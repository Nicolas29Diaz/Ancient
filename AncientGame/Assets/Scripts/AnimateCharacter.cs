using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCharacter : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void AnimateRun(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isWalking", false);
    }

    public void AnimateWalk(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", false);
    }
}
