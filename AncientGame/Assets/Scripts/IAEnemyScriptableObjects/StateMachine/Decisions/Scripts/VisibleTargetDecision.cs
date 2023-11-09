using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/TargetNotVisible")]
public class VisibleTargetDecision : Decision
{
    public override bool Decide(StateMachineEnemy controller)
    {
        return IsTargetNotVisible(controller);
    }
    private bool IsTargetNotVisible(StateMachineEnemy controller)
    {
        controller.transform.Rotate(0, controller.enemyStats.searchingTurningSpeed * Time.deltaTime, 0);
        return controller.HasTimeElapsed(controller.enemyStats.searchDuration);
    }

}
