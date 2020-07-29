using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;

        if(hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        
        isDead = true;
        animator.SetTrigger("die");
        navMeshAgent.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public bool IsDead()
    {
        return isDead;
    }
}
