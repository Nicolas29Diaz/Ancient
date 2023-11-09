using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CurrentState
{
    Patrol,
    Attack,
}
public class EnemyAnimations : MonoBehaviour
{
    public AnimateCharacter animateCharacter;
    public NavMeshAgent agent;
    public CurrentState currentState;

    private void Awake()
    {
        animateCharacter = GetComponent<AnimateCharacter>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if((Mathf.Abs(agent.velocity.x) > 0.2f) || (Mathf.Abs(agent.velocity.z) > 0.2f))
        {
            if(currentState == CurrentState.Attack)
            {
                animateCharacter.AnimateRun(true);
            } else if (currentState == CurrentState.Patrol){
               animateCharacter.AnimateWalk(true);
            }
            else
            {
                animateCharacter.AnimateRun(false);
            }
        }
    }
}
