using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamageable {

    private NavMeshAgent agent = null;
    private float health = 150f;
    private Animator anim = null;

    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            agent.speed = 0;

            anim.SetTrigger("Death");

            //Destroy(gameObject);
        }
    }

}
