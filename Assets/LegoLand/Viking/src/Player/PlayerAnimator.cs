using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Player player;
    private Movement movement;

    private void Awake()
    {
        player = GetComponent<Player>();
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }

    public void OnMovement(float vertical)
    {
        animator.SetFloat("Vertical", vertical);
    }

    public void OnRoll()
    {
        animator.SetTrigger("isRoll");
    }

    public void EndRoll()
    {
        movement.isRoll = false;
        player.isButtonRoll = false;
    }

    public void OnFire()
    {
        animator.SetTrigger("doShot");
    }
}
