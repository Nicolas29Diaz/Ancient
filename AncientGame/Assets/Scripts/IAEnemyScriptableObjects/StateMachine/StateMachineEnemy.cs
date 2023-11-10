using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;


public class StateMachineEnemy : MonoBehaviour
{
    public EnemyStats enemyStats;
    public States currentState;
    public States remainState;

    public NavMeshAgent agent;
    public List<Transform> waypoints;
    public int nextWaypoint;
    public Transform target;
    public Vector3 lastKnowTargetPosition;
    public bool stateBoolVariable;
    public float stateTimeElapsed;

    private bool _isActive;


    




    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>(); 
        
    }

    private void Update()
    {
        if (!_isActive) return;
        currentState.UpdateState(this);
    }

    public void InitializeIA(bool activate, List<Transform> waypointList)
    {
        waypoints = waypointList;
        _isActive = activate;
        agent.enabled = _isActive;
    }

    public void TransicionToState(States nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool HasTimeElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        if (stateTimeElapsed >= duration)
        {
            stateTimeElapsed = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnExitState()
    {
        stateBoolVariable = false;
        stateTimeElapsed = 0;
    }



    private void OnDrawGizmos()
    {
        if(currentState != null)
        {
            Gizmos.color = currentState.gizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        } 
    }

}
