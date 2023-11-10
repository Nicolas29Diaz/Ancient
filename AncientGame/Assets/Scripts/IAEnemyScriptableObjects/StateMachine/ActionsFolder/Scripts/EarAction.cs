using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(menuName = "AI/ActionMenu/Ear")]
public class EarAction : Action
{

    public override void Act(StateMachineEnemy controller)
    {
        HearNoise(controller);
    }


    public void HearNoise(StateMachineEnemy controller)
    {

        ListeningRange fov = controller.GetComponent<ListeningRange>();
        if (fov != null)
        {
            if (fov.possibleTarget != null)
            {
                controller.agent.destination = fov.possibleTarget.position;
                controller.agent.Resume();


            }
        }
    }
}

