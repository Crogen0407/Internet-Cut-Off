using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;
    private PoolManager _poolManager;

    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    public void Attack(float attackDamage, float attackDelay, Vector2 attackDirection)
    {
        StartCoroutine(AttackCoroutine(attackDamage, attackDelay, attackDirection));
    }

    private IEnumerator AttackCoroutine(float attackDamage, float attackDelay, Vector2 attackDirection)
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        Rigidbody2D bullet = _poolManager.Pop("EnemyBullet", transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet.AddForce(attackDirection * 10, ForceMode2D.Impulse);
        isAttacking = false;
    }
}
