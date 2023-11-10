using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/ActionMenu/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateMachineEnemy controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateMachineEnemy controller)
    {
        EnemyAnimations enemy = controller.GetComponent<EnemyAnimations>();
        enemy.currentState = CurrentState.Patrol;
        controller.agent.speed = controller.enemyStats.walSpeed;
        controller.agent.destination = controller.waypoints[controller.nextWaypoint].position;
        controller.agent.Resume();
        
        if(controller.agent.remainingDistance <= controller.agent.stoppingDistance && !controller.agent.pathPending)
        {
            controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.waypoints.Count;
        }
        
        
    }

    

}
