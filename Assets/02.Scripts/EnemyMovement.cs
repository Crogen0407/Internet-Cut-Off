using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed, Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
    }
}
