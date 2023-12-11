using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;
    
    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
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

    public void OnEvent()
    {
        
    }
}
