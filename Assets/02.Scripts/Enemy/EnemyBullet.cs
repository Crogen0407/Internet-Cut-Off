using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PoolManager _poolManager;
    [SerializeField] private float _lifeTime = 10;
    [SerializeField] private string _bulletType;
    
    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    private void OnEnable()
    {
        StopCoroutine(OnDestroyCoroutine(_lifeTime));
        StartCoroutine(OnDestroyCoroutine(_lifeTime));
    }

    private IEnumerator OnDestroyCoroutine(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        _poolManager.Push(_bulletType, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthSystem healthSystem = other.GetComponent<HealthSystem>();
        if (other.CompareTag("Player") || other.CompareTag("Untagged"))
        {
            if (healthSystem != null)
            {
                healthSystem.Hp -= 10; 
            }   

            if (_bulletType.Equals("EnemyBullet"))
            {
              _poolManager.Push(_bulletType, gameObject);
            }
        }
    }
}
