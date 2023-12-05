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
        _enemyInfoData.healthSystem = new HealthSystem(3, () => { Debug.Log("으흑"); }, () => { Debug.Log("죽고싶지 않아"); });
        _enemyInfoData.healthSystem.Hp-=3;
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
