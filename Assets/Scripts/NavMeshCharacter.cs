using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TimeObjects;

public class NavMeshCharacter : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Transform target;
    public RewindObject rewindable;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rewindable = GetComponent<RewindObject>();
    }

    private void Update()
    {
        if (target)
            navMeshAgent.destination = target.position;
        animator.SetFloat("speed", navMeshAgent.speed);
        animator.SetFloat("angularspeed", navMeshAgent.angularSpeed);
        animator.SetBool("isrewinding", rewindable.isRewinding);
    }
}
