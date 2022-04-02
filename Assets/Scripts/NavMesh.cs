using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    [SerializeField] public Transform moveTransform;
    private NavMeshAgent navMeshAgent;
    private Animator _animator;
    public bool isAttacking;

    private int _animIDAttacking;
    private int _animIDMoveSet;
    private int _animIDAttackSet;

    [SerializeField] private GameObject RHandCollider;
    [SerializeField] private GameObject LHandCollider;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _animIDAttacking = Animator.StringToHash("isAttacking");
        _animIDMoveSet = Animator.StringToHash("MoveSet");
        _animIDAttackSet = Animator.StringToHash("AttackSet");

        navMeshAgent.speed = RandomSpeed();

    }

    private void Update()
    {
        navMeshAgent.destination = moveTransform.position;
        CheckAttack();
        CheckRotationAndStop();
    }

    private void CheckAttack()
    {
        // Check if we've reached the destination
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        _animator.SetBool(_animIDAttacking, true);
    }
    private float RandomSpeed()
    {
        // should pick a random speed and a suitable animation for it
        // currently using defaults
        return 5f;
    }

    public void Move()
    {
        navMeshAgent.isStopped = false;
    }
    public void EndAttack()
    {
        _animator.SetBool(_animIDAttacking, false);

        // randomize next attack animation
        //float[] set = new float[] {0f,0.33f,0.66f,1f};
        float[] set = new float[] {0.66f,1f};
        int rnd = Random.Range(0, set.Length);
        _animator.SetFloat(_animIDAttackSet, set[rnd]);
    }

    public void AttackColliderAct()
    {
        isAttacking = true;
    }
    public void AttackColliderDeact()
    {
        isAttacking = false;
    }

    // Activate and deactivate collider boxes---
    #region colliderBoxes
    public void ActivateRHandCollider()
    {
        RHandCollider.SetActive(true);
        AttackColliderAct();
    }
    public void DeactivateRHandCollider()
    {
        RHandCollider.SetActive(false);
        AttackColliderDeact();
    }
    public void ActivateLHandCollider()
    {
        LHandCollider.SetActive(true);
        AttackColliderAct();
    }
    public void DeactivateLHandCollider()
    {
        LHandCollider.SetActive(false);
        AttackColliderDeact();
    }
    #endregion
    // ------------------
    private void CheckRotationAndStop()
    {
        if (!(navMeshAgent.velocity == Vector3.zero)) return;

        gameObject.transform.LookAt(moveTransform);
    }

}
