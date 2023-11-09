using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Transform> waypoints;
    private StateMachineEnemy[] _controllers;

    private void Start()
    {
        _controllers = FindObjectsOfType<StateMachineEnemy>();
        foreach (var controller in _controllers)
        {
            controller.InitializeIA(true, waypoints);
        }
    }
}
