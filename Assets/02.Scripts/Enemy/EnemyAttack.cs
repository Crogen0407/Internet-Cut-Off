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

        if (_enemy.enemyInfoData.enemyType != EnemyType.Red)
        {
            Rigidbody2D bullet = _enemy.poolManager.Pop("EnemyBullet", transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bullet.AddForce(attackDirection * 10, ForceMode2D.Impulse);
        }
        else
        {
            _enemy.poolManager.Pop("EnemyExplosion", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Sound_shot();
        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        _enemy.enemyAnimation.SetAnimationState("Attack", false);
    }

    void Sound_attack()
    {
        //원본프리펩 파괴 방지를 위한 복사
        var copy = Instantiate(S_attack, this.transform.position, this.transform.rotation);
        //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
        Destroy(copy, 0.5f);
    }
    void Sound_shot()
    {
        //원본프리펩 파괴 방지를 위한 복사
        var copy = Instantiate(S_shot, this.transform.position, this.transform.rotation);
        //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
        Destroy(copy, 0.5f);
    }
}
