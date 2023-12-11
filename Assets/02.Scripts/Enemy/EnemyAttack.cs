using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    public bool isAttacking;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void Attack(float attackDamage, float attackDelay, Vector2 attackDirection)
    {
        StartCoroutine(AttackCoroutine(attackDamage, attackDelay, attackDirection));
    }

    private IEnumerator AttackCoroutine(float attackDamage, float attackDelay, Vector2 attackDirection)
    {
        isAttacking = true;
        _enemy.enemyAnimation.SetAnimationState("Attack", isAttacking);
        yield return new WaitForSeconds(attackDelay);
        Rigidbody2D bullet = _enemy.poolManager.Pop("EnemyBullet", transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet.AddForce(attackDirection * 10, ForceMode2D.Impulse);
        isAttacking = false;
        _enemy.enemyAnimation.SetAnimationState("Attack", isAttacking);
    }
}
