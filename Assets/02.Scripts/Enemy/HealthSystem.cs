using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _hp = 3;
    public Action Dead;
    public Action Damaged;

    ~HealthSystem()
    {
        try
        {
            Dead = null;
            Damaged = null;
        }
        catch
        {
            Debug.Log("이거 Null로 하면 안될듯??");
        }
        
    }
    
    public int Hp
    {
        get => _hp;
        set
        {
            int currentHp = _hp;
            _hp = value;
            if (currentHp > _hp)
            {
                Damaged?.Invoke();
            }
            if (_hp == 0)
            {
                Dead?.Invoke();
            }
        }
    }
}
