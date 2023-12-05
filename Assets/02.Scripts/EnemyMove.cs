using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SO_EnemyInfoData _enemyInfoData;
    
    private void Awake()
    {
        _enemyInfoData.healthSystem.Hp--;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
