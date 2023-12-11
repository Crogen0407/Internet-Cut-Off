using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy _enemy;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] GameObject S_move;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = _enemy.visualTransform.GetComponent<SpriteRenderer>();

    }

    public void Move(float speed, Vector2 direction)
    {
        SoundRandom();
        _rigidbody.velocity = new Vector2(direction.normalized.x * speed, _rigidbody.velocity.y);
        _enemy.currentVelocity = _rigidbody.velocity.x;
    }
    void SoundRandom()
    {
        int ran = UnityEngine.Random.Range(0, 60);
        if (ran == 5)
        {
            //원본프리펩 파괴 방지를 위한 복사
            var copy = Instantiate(S_move, this.transform.position, this.transform.rotation);
            //원본 프리펩 파괴가 아닌, 복사된 오브젝트 파괴
            Destroy(copy, 0.5f);
        }
    }
}
