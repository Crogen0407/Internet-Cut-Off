using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = _enemy.visualTransform.GetComponent<SpriteRenderer>();

    }

    public void Move(float speed, Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
        _enemy.currentVelocity = _rigidbody.velocity.x;
        _spriteRenderer.flipX = _rigidbody.velocity.x > 0;
    }
}
