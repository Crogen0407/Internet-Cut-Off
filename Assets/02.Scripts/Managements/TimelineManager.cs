using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimelineManager : MonoSingleton<TimelineManager>
{
    public List<TimeLineFrame> timeline;
    public int TimeLineIndexer { get; private set; }
    
    
    
    //Controllers
    [HideInInspector] public ScreenEffectController screenEffectController;
    [HideInInspector] public CutSceneController cutSceneController;
    [HideInInspector] public Player player;
    [HideInInspector] public Transform canvasTransform;

    private void Awake()
    {
        screenEffectController = FindObjectOfType<ScreenEffectController>();
        cutSceneController = FindObjectOfType<CutSceneController>();
        player = FindObjectOfType<Player>();
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    private void Start()
    {
        StartCoroutine(StartTimeline());
    }

    private IEnumerator StartTimeline()
    {
        for (int i = 0; i < timeline.Count; i++)
        {
            yield return new WaitForSeconds(timeline[i].delayTime);
            timeline[i].frameEvent?.Invoke();
        }
    }
}

[Serializable]
public class TimeLineFrame
{
    [Tooltip("현재 이벤트가 실행되기 전의 delay 시간")]
    public float delayTime;
    public UnityEvent frameEvent;
}