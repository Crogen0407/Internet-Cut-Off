using System;
using Cinemachine;
using UnityEngine;

public class CinemachineController : MonoBehaviour
{
    [SerializeField]private CinemachineConfiner2D _cinemachineConfiner;

    private void Awake()
    {
        _cinemachineConfiner = FindObjectOfType<CinemachineConfiner2D>();
    }

    public void SetCinemachineConfinerBoundingShape(PolygonCollider2D BoundingShape)
    {
        _cinemachineConfiner.m_BoundingShape2D = BoundingShape;
    }
}
