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

    public void SetCinemachinePriority(float waitTime, float duration, float latePriority)
    {
        StartCoroutine(SetCinemachinePriorityCoroutine(waitTime, duration, latePriority));
    }

    private IEnumerator SetCinemachinePriorityCoroutine(float waitTime, float duration, float latePriority)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float currentPriority = _virtualCamera.Priority;
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentPriority > latePriority ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            if (fadeMode == true)
            {
                _virtualCamera.Priority = (int)percentTime;
            }
            else
            {
                _virtualCamera.Priority = (int)(currentPriority - percentTime);
            }
            yield return null;
        }
    }
}
