using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private PoolManager _poolManager;
    
    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    private void OnEnable()
    {
        StopCoroutine(OnDestroyCoroutine(10));
        StartCoroutine(OnDestroyCoroutine(10));
    }

    private IEnumerator OnDestroyCoroutine(float destroyDelay)
    {
        yield return new WaitForSeconds(destroyDelay);
        _poolManager.Push("EnemyBullet", gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Untagged"))
        {
            _poolManager.Push("EnemyBullet", gameObject);
        }
    }
}
