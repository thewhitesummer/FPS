﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    Transform target;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;

    void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

    void Update() {
        if(health.IsDead()) {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(isProvoked) {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange) {
            isProvoked = true; 
        }
    }

    private void EngageTarget() {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance) {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance) {
            AttackTarget();
        }
    }

    private void AttackTarget() {
        GetComponent<Animator>().SetBool("Attack", true);
    }

    private void ChaseTarget() {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken() {
        isProvoked = true;
    }
    
}
