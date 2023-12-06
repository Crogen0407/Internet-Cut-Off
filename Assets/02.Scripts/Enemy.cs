using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private SO_EnemyInfoData _enemyInfoData;
    [SerializeField] private LayerMask _layerMask;
    public bool isFindPlayer;
    
    //Components
    private HealthSystem _healthSystem;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        _healthSystem.Damaged += () =>
        {
            Debug.Log("아야");
        };
        _healthSystem.Dead += () =>
        {
            Debug.Log("밍...폭발");
        };
    }

    private void Update()
    {
        Collider2D colliders = Physics2D.OverlapCircle(transform.position, _enemyInfoData.maxAttackRangeRadius, _layerMask);
        if (colliders != null && _enemyAttack.isAttacking == false)
        {
            _enemyAttack.Attack(_enemyInfoData.attackDamage, _enemyInfoData.attackDelay);
        }
    }

    private void FixedUpdate()
    {
        
        if (_enemyAttack.isAttacking == false)
        {
            _enemyMovement.Move(_enemyInfoData.moveSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _enemyInfoData.maxAttackRangeRadius);
    }
}
