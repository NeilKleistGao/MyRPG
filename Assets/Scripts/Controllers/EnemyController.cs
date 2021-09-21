using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates {
    GUARD, PATROL, CHASE, DEAD
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour {
    public EnemyStates enemyStates;

    private NavMeshAgent agent;

    [Header("Basic Settings")]
    public float sightRadius;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        SwitchStates();
    }

    private void SwitchStates() {
        if (FindPlayer()) {
            enemyStates = EnemyStates.CHASE;
        }

        switch (enemyStates) {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                break;
            case EnemyStates.CHASE:
                break;
            case EnemyStates.DEAD:
                break;
        }
    }

    private bool FindPlayer() {
        var colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach (var collider in colliders) {
            if (collider.CompareTag("Player")) {
                return true;
            }
        }

        return false;
    }
}
