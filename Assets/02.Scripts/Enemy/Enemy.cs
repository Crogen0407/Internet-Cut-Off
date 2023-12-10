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
    
    public float currentVelocity;
    
    //Components
    private HealthSystem _healthSystem;
    
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    internal EnemyAnimation enemyAnimation;
    
    internal SpriteRenderer spriteRenderer;
    internal Rigidbody2D rigidbody;
    internal Animator animator;

    //Managers
    internal PoolManager poolManager;

    
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
        
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimation = GetComponent<EnemyAnimation>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
        poolManager = PoolManager.Instance;
        
        switch (_enemyInfoData.enemyType)
        {
            //나중에 컬러값 예쁘게 바꾸기
            case EnemyType.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case EnemyType.Red:
                spriteRenderer.color = Color.red;
                break;
            case EnemyType.Blue:
                spriteRenderer.color = Color.blue;
                break;
        }
        
        _healthSystem.Damaged += () =>
        {
            Debug.Log("아야");
        };
        _healthSystem.Dead += () =>
        {
            //나중에 오브젝트 풀링으로 바꾸기
            Destroy(gameObject);
        };
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _enemyInfoData.viewingRadius, _layerMask);
        if (collider != null)
        {
            Vector2 myPositionToPlayerPositionDirection = (collider.transform.position - transform.position).normalized;
            RaycastHit2D hit;
            int layerMask2 = ~(LayerMask.GetMask("Enemy") | LayerMask.GetMask("EnemyBullet") | LayerMask.GetMask("None"));
                
            hit = Physics2D.Raycast(transform.position, myPositionToPlayerPositionDirection, _enemyInfoData.viewingRadius, layerMask2);
            if (hit.transform != null && hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, myPositionToPlayerPositionDirection * _enemyInfoData.viewingRadius);
                if (Vector2.Distance(collider.transform.position, transform.position) < _enemyInfoData.maxAttackRangeRadius)
                {
                    if (_enemyAttack.isAttacking == false)
                    {
                        _enemyAttack.Attack(_enemyInfoData.attackDamage, _enemyInfoData.attackDelay, myPositionToPlayerPositionDirection);
                    }
                }
                else
                {
                    _enemyMovement.Move(_enemyInfoData.moveSpeed, myPositionToPlayerPositionDirection);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _enemyInfoData.maxAttackRangeRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyInfoData.viewingRadius);
    }
}
