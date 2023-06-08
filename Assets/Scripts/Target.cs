using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour, IDamageable {

    private NavMeshAgent agent = null;
    private float health = 150f;
    private Animator anim = null;
    public float despawnTime = 10;

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

    void Update() {
        if (health <= 0) {

            despawnTime -= Time.deltaTime;
            if (despawnTime < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Car")
        {
            health -= 160;
            agent.speed = 0;

            anim.SetTrigger("Death");
        }
    }

}
