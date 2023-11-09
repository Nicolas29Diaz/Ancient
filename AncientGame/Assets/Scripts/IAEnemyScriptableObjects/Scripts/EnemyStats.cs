using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float walSpeed;
    public float runSpeed;
    public float attackRate;
    public int searchDuration;
    public int searchingTurningSpeed;
}
