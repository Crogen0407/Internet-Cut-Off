using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    public SO_EnemyInfoData enemyInfoData;
    [SerializeField] private LayerMask _layerMask;
    public bool isFindPlayer;
    
    public float currentVelocity;
    
    [HideInInspector]public Transform visualTransform;
    
    //Components
    [HideInInspector]public HealthSystem healthSystem;
    public Interaction Interaction { get; private set; }
    [HideInInspector]public EnemyAttack enemyAttack;
    [HideInInspector]public EnemyMovement enemyMovement;
    [HideInInspector]public EnemyAnimation enemyAnimation;
    
     private SpriteRenderer _spriteRenderer;

    //Managers
    [HideInInspector]public PoolManager poolManager;

    
    private void Awake()
    {
        visualTransform = transform.Find("Visual");
        healthSystem = GetComponent<HealthSystem>();
        
        enemyAttack = GetComponent<EnemyAttack>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAnimation = visualTransform.GetComponent<EnemyAnimation>();
        try
        {
            Interaction = GetComponent<Interaction>();
        }
        catch (NullReferenceException e)
        {
            
        }
        _spriteRenderer = visualTransform.GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        poolManager = PoolManager.Instance;
        switch (enemyInfoData.enemyType)
        {
            //나중에 컬러값 예쁘게 바꾸기
            case EnemyType.Yellow:
                _spriteRenderer.color = Color.yellow;
                break;
            case EnemyType.Red:
                _spriteRenderer.color = Color.red;
                break;
            case EnemyType.Blue:
                _spriteRenderer.color = Color.blue;
                break;
        }
        
        healthSystem.Damaged += () =>
        {
            Debug.Log("아야");
        };
        healthSystem.Dead += () =>
        {
            if (GameManager.Instance.StageController.CurrentStage > 4)
            {
                GameManager.Instance.StageController.monsterIndex++;
            }
            //나중에 오브젝트 풀링으로 바꾸기
            if (Interaction != null)
            {
                Interaction?.action();
            }
            Destroy(gameObject);
        };
    }

    private void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, enemyInfoData.viewingRadius, _layerMask);
        if (collider != null)
        {
            Vector2 myPositionToPlayerPositionDirection = (collider.transform.position - transform.position).normalized;
            RaycastHit2D hit;
            int layerMask2 = ~(LayerMask.GetMask("Enemy") | LayerMask.GetMask("EnemyBullet") | LayerMask.GetMask("None"));
                
            hit = Physics2D.Raycast(transform.position, myPositionToPlayerPositionDirection, enemyInfoData.viewingRadius, layerMask2);
            if (hit.transform != null && hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, myPositionToPlayerPositionDirection * enemyInfoData.viewingRadius);
                if (Vector2.Distance(collider.transform.position, transform.position) < enemyInfoData.maxAttackRangeRadius)
                {
                    if (enemyAttack.isAttacking == false)
                    {
                        currentVelocity = 0;
                        enemyAttack.Attack(enemyInfoData.attackDamage, enemyInfoData.attackDelay, myPositionToPlayerPositionDirection);
                    }
                }
                else
                {
                    enemyMovement.Move(enemyInfoData.moveSpeed, myPositionToPlayerPositionDirection);
                }
                _spriteRenderer.flipX = myPositionToPlayerPositionDirection.x > 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, enemyInfoData.maxAttackRangeRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyInfoData.viewingRadius);
    }
}
