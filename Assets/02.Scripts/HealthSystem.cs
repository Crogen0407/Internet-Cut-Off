using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int _hp;
    public Action Dead;
    public Action Damaged;
    public int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            int currentHp = _hp;
            if (currentHp > value)
            {
                Debug.Log("Damage");
                Damaged?.Invoke();
            }
            if (_hp <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}
