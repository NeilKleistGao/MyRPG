using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    private NavMeshAgent agent;
    private Animator animator;

    private GameObject attackTarget;
    private float lastAttackTime;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        MouseManager.instance.onMouseClicked += MoveToTarget;
        MouseManager.instance.onEnemyClicked += MoveToAttackTarget;
    }

    private void Update() {
        SwitchAnimation();
        lastAttackTime -= Time.deltaTime;
    }

    private void SwitchAnimation() {
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }

    private void MoveToTarget(Vector3 target) {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    private void MoveToAttackTarget(GameObject gameObject) {
        if (gameObject != null) {
            attackTarget = gameObject;
            StartCoroutine(MoveCoroutine());
        }
    }

    private IEnumerator MoveCoroutine() {
        agent.isStopped = false;
        transform.LookAt(attackTarget.transform);
        
        while (Vector3.Distance(transform.position, attackTarget.transform.position) > 1) {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }

        agent.isStopped = true;
        if (lastAttackTime < 0) {
            animator.SetTrigger("Attack");
            lastAttackTime = 0.5f;
        }
    }
}
