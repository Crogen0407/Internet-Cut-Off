using System;
using UnityEngine;

public class EnemyAnimation : Enemy
{
    public void ChangeAnimationState(string parameterName, int settingValue)
    {
        animator.SetBool(parameterName, Convert.ToBoolean(settingValue));
    }

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(currentVelocity));
    }
}
