using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class States : ScriptableObject
{
    // Start is called before the first frame update
    public Action[] actions;
    public Transition[] transitions;
    public Color gizmoColor = Color.blue; //Si no esta activo lo veremos azul

    public void UpdateState(StateMachineEnemy controller)
    {
        ExecuteActions(controller);
        CheckForTransitions(controller);
    }

    private void ExecuteActions(StateMachineEnemy controller)
    {
        foreach (var action in actions)
        {
            action.Act(controller);
        }
    }
    private void CheckForTransitions(StateMachineEnemy controller)
    {
        foreach (var transition in transitions)
        {
            bool decision = transition.decision.Decide(controller);
            if (decision)
            {
                controller.TransicionToState(transition.trueState);
            }
            else
            {
                controller.TransicionToState(transition.falseState);
            }
        }
    }
}
