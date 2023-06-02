using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Animator anim = null;
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance = 3;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= stoppingDistance)
        {
            transform.LookAt(target);

            //Vector3 direction = target.position - transform.position;
            //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            //transform.rotation = rotation;

            anim.SetFloat("Speed", 0f);
            anim.SetTrigger("Attack");

        }

    }

}
