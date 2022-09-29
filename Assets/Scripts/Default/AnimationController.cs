using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    public AnimationState animationState;
    public Transform LookTarget;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void SetXY(float x, float y)
    {
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }
    public EventHandler OnAttackEvent;
    public void OnAttack()
    {
        OnAttackEvent?.Invoke(this, EventArgs.Empty);
    }

    internal void SetSpeed(float value)
    {
        animator.SetFloat("speed", value);
    }
    public void Die()
    {
        animator.SetBool("death", true);
    }
    public EventHandler OnSpearShoot;
    public void SpearShoot()
    {
        OnSpearShoot?.Invoke(this, EventArgs.Empty);
    }

    internal void SetWalk(float value)
    {
        animator.SetFloat("walk", value);
    }

    public void Stretch(bool value)
    {
        animator.SetBool("stretch", value);
    }
    public void Set8WayLayerWeight(bool value)
    {
        if (value)
        {
            animator.SetLayerWeight(0, 1);
            animator.SetLayerWeight(1, 0);
        }
        else
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }
    }

    internal void Dance()
    {
        animator.SetTrigger("dance");
    }
    internal void Sword()
    {
        animator.SetTrigger("sword");
    }

    internal void Jump()
    {
        // animationState = AnimationState.Jump;
        // ResetJump();
        // animator.ResetTrigger("down");
        animator.SetTrigger("jump");
    }
}
public enum AnimationState
{
    Other, Idle, Walk, Run, Jump, Climb, Fall, Kick, Slide, HandJump
}
