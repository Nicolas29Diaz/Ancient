using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Ear")]
public class EarDecision : Decision
{
    public override bool Decide(StateMachineEnemy controller)
    {
        return Ear(controller);
    }

    private bool Ear(StateMachineEnemy controller)
    {
        
        ListeningRange fov = controller.GetComponent<ListeningRange>();
        if (fov == null) { return false; }
        if (fov.possibleTarget != null)
        {
            controller.target = fov.possibleTarget;

            return true;
        }
        return false;
    }
}
