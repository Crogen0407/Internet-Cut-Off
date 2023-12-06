using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;
    
    public void Attack(float attackDamage, float attackDelay)
    {
        StartCoroutine(AttackCoroutine(attackDamage, attackDelay));
    }

    private IEnumerator AttackCoroutine(float attackDamage, float attackDelay)
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("공격");
        isAttacking = false;
    }
}
