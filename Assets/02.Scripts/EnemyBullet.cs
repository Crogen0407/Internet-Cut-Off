using System;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public Vector2 fireDirection;
    public float firePower = 5;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        try
        {
            _rigidbody.velocity = fireDirection;
        }
        catch (NullReferenceException e)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}
