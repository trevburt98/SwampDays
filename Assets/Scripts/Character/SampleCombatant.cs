using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleCombatant : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolLocations;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float minIdleTime;
    [SerializeField] private float maxIdleTime;
    private enum CombatState{
        Posted,
        Patrolling,
        Suspicious,
        Aware,
        Searching,
        seekingCover,
        inCover,
        Advancing,
        Retreating,
        Fleeing
    }
    private static System.Random random = new System.Random();
    private CombatState state = CombatState.Posted;
    private float timeSinceUpdate = 0;
    private float timeToUpdate = 0;
    private bool becameIdle = false;
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        switch(state){
            case CombatState.Posted:
                if(timeSinceUpdate >= timeToUpdate){
                    int r = random.Next(patrolLocations.Count);
                    agent.SetDestination(patrolLocations[r].position);
                    timeSinceUpdate = 0;
                    state = CombatState.Patrolling;
                }
                break;
            case CombatState.Patrolling:
                if (!agent.hasPath){
                    state = CombatState.Posted;
                    timeSinceUpdate = 0;
                    timeToUpdate = (float)random.NextDouble()*maxIdleTime+minIdleTime;
                }
                break;
        }
    }
}
