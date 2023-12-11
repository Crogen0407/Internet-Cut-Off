using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy _enemy;
    public bool isAttacking;

    [SerializeField] GameObject S_attack;
    [SerializeField] GameObject S_shot;

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
        Sound_attack();
        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);
        _enemy.enemyAnimation.SetAnimationState("Attack", true);
        yield return new WaitForSeconds(0.3f);

        
        Rigidbody2D bullet = _enemy.poolManager.Pop("EnemyBullet", transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        bullet.AddForce(attackDirection * 10, ForceMode2D.Impulse);
        Sound_shot();
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        _enemy.enemyAnimation.SetAnimationState("Attack", false);
    }

    void Sound_attack()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_attack, this.transform.position, this.transform.rotation);
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(copy, 0.5f);
    }
    void Sound_shot()
    {
        //���������� �ı� ������ ���� ����
        var copy = Instantiate(S_shot, this.transform.position, this.transform.rotation);
        //���� ������ �ı��� �ƴ�, ����� ������Ʈ �ı�
        Destroy(copy, 0.5f);
    }
}
