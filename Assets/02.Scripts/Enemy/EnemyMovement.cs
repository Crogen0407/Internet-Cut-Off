using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rigidbody = _enemy.rigidbody;
    }

    public void Move(float speed, Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
        _enemy.currentVelocity = _rigidbody.velocity.x;
        _enemy.spriteRenderer.flipX = _rigidbody.velocity.x > 0;
    }
}
