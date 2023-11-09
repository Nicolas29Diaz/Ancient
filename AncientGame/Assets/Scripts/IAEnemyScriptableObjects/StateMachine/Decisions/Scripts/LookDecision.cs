using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Look")]
public class LookDecision : Decision
{
    
    public override bool Decide(StateMachineEnemy controller)
    {
        return Look(controller);
    }

    private bool Look(StateMachineEnemy controller)
    {
        FieldOfView fov = controller.GetComponent<FieldOfView>();
        if (fov == null) { return false; }
        if(fov.visibleTarget != null && fov.visibleTarget.GetComponent<PlayerController1>())
        {
            controller.target = fov.visibleTarget;
            
            return true;
        }
        return false;
    }

}
