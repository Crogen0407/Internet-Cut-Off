using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    private CinemachineConfiner2D _cinemachineConfiner;
    private CinemachineVirtualCamera _virtualCamera;
    
    private void Awake()
    {
        _cinemachineConfiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    public void SetCinemachineConfinerBoundingShape(PolygonCollider2D BoundingShape)
    {
        _cinemachineConfiner.m_BoundingShape2D = BoundingShape;
    }
}
