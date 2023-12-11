using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    
    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }
    
    public void ChangeAnimationState(string parameterName, int settingValue)
    {
        _animator.SetBool(parameterName, Convert.ToBoolean(settingValue));
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_enemy.currentVelocity));
        
    }
}
