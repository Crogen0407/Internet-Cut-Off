using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimationState(string parameterName, int settingValue)
    {
        _animator.SetBool(parameterName, Convert.ToBoolean(settingValue));
    }
    
    public void SetAnimationState(string parameterName, bool settingValue)
    {
        _animator.SetBool(parameterName, settingValue);
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_enemy.currentVelocity));
    }
}
