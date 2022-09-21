using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bot : Character
{
    [SerializeField] List<Collect> myCollect;
    public Transform Target;
    [SerializeField] Transform Chest;
    private void Start()
    {
        Target = FindObjectOfType<Player>().transform;
        // movement.GoToPosition(Target);
        animationController.Sword();
        // animationController.Set8WayLayerWeight(false);
        animationController.OnAttackEvent += OnAttack;
    }

    private void OnAttack(object sender, EventArgs e)
    {
        if (Target.GetComponent<Character>().IsAlive && IsAlive)
        {

            if (Vector3.Distance(transform.position, Target.position) < 6)
            {

                Target.GetComponent<Character>().TakeDamage(-20);
            }
            movement.GoToPosition(Target);
            Attacking = false;
        }
    }
    bool Attacking = false;

    // private void Update()
    // {
    //     if (Vector3.Distance(Target.position, transform.position) < 4 && !Attacking && IsAlive)
    //     {
    //         Attacking = true;
    //         animationController.Attack();
    //         movement.Cancel();
    //     }
    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.GetComponent<Spear>())
    //     {
    //         other.transform.SetParent(Chest);
    //         other.transform.GetComponent<Spear>().Stabbed();
    //         rb.AddForce(other.relativeVelocity.normalized * 300);
    //         TakeDamage(-60);
    //     }
    // }
}