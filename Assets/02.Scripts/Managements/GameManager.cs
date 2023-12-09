using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public StageController stageController;
    [HideInInspector] public CinemachineController cinemachineController;
    [HideInInspector] public ScreenEffectController screenEffectController;
    
    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
        cinemachineController = FindObjectOfType<CinemachineController>();
        screenEffectController = FindObjectOfType<ScreenEffectController>();
    }

    private void Start()
    {
        stageController.StartFirstStage();
        screenEffectController.SetScreenEffect("_LatterboxCurrentSize", 0);
    }

    private void Update()
    {
        
    }
}
