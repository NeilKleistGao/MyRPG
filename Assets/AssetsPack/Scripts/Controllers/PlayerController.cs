using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        MouseManager.instance.onMouseClicked += MoveToTarget;
    }

    private void Update() {
        SwitchAnimation();
    }

    private void SwitchAnimation() {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    private void MoveToTarget(Vector3 target) {
        agent.destination = target;
    }
}
