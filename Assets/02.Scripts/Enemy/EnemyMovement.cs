using UnityEngine;

public class EnemyMovement : Enemy
{
    public void Move(float speed, Vector2 direction)
    {
        rigidbody.velocity = new Vector2(direction.normalized.x * speed, rigidbody.velocity.y);
        currentVelocity = rigidbody.velocity.x;
        spriteRenderer.flipX = rigidbody.velocity.x > 0;
    }
}
