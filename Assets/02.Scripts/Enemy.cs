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
    private Rigidbody2D _rigidbody;
    private HealthSystem _healthSystem;
    private EnemyAttack _enemyAttack;
    private EnemyMovement _enemyMovement;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthSystem = GetComponent<HealthSystem>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Start()
    {
        switch (_enemyInfoData.enemyType)
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
            default:
                throw new ArgumentOutOfRangeException();
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

    private void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _enemyInfoData.viewingRadius, _layerMask);
        if (collider != null && _enemyAttack.isAttacking == false)
        {
            //_enemyAttack.Attack(_enemyInfoData.attackDamage, _enemyInfoData.attackDelay);
            _rigidbody.velocity = new Vector2((collider.transform.position - transform.position).normalized.x * _enemyInfoData.moveSpeed, _rigidbody.velocity.y);
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
        Gizmos.DrawWireSphere(transform.position, _enemyInfoData.viewingRadius);
    }
}
