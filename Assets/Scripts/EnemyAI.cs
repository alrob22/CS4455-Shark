using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator anim;
    public GameObject player;
    public Transform patrolPos;
    public float patrolSpeed = 45f;
    public float chaseDist = 35f;
    public float attackDist = 15f;

    public enum AIState
    {
        Patrol, Chase, Attack
    }
    public AIState state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        state = AIState.Patrol;
    }

    void Update()
    {
        switch (state)
        {
            case AIState.Patrol:
                if (Vector3.Distance(player.transform.position, agent.transform.position) < chaseDist)
                {
                    state = AIState.Chase;
                }

                agent.isStopped = true;
                agent.transform.RotateAround(patrolPos.position, Vector3.up, Time.deltaTime * patrolSpeed);

                break;

            case AIState.Chase:
                if (Vector3.Distance(player.transform.position, agent.transform.position) > chaseDist)
                {
                    state = AIState.Patrol;
                } else if (Vector3.Distance(player.transform.position, agent.transform.position) < attackDist)
                {
                    state = AIState.Attack;
                    anim.SetBool("AttackBool", true);
                }

                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                break;

            case AIState.Attack:
                if (Vector3.Distance(player.transform.position, agent.transform.position) > attackDist)
                {
                    state = AIState.Chase;
                    anim.speed = 1;
                    anim.SetBool("AttackBool", false);
                }

                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                break;
        }
    }
}
