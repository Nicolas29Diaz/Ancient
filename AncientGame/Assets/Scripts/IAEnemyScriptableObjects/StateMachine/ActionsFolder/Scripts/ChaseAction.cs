using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/ActionMenu/Chase")]
public class ChaseAction : Action
{
    
    public override void Act(StateMachineEnemy controller)
    {
        Chase(controller);
    }

    private void Chase(StateMachineEnemy controller)
    {
        EnemyAnimations enemy = controller.GetComponent<EnemyAnimations>();
        enemy.currentState = CurrentState.Attack;
        controller.agent.speed = controller.enemyStats.runSpeed;
        
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if(fov != null) {
        if(fov.visibleTarget != null)
            {
                controller.agent.destination = controller.target.position;
                controller.lastKnowTargetPosition = controller.target.position;
                controller.agent.Resume();
            }
        else
            {
                controller.agent.destination = controller.lastKnowTargetPosition;
                controller.agent.Resume();
            }
        }
    }
}
