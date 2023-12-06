using System;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string parameterName, int settingValue)
    {
        _animator.SetBool(parameterName, Convert.ToBoolean(settingValue));
    }
}
