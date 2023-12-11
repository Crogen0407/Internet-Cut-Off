using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    //Controller
    [HideInInspector] public StageController stageController;
    [HideInInspector] public CinemachineController cinemachineController;
    [HideInInspector] public ScreenEffectController screenEffectController;
    [HideInInspector] public VolumeController volumeController;
    [HideInInspector] public Player player;
    
    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
        cinemachineController = FindObjectOfType<CinemachineController>();
        screenEffectController = FindObjectOfType<ScreenEffectController>();
        volumeController = FindObjectOfType<VolumeController>();
        
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        stageController.StartFirstStage();
    }

    private void Update()
    {
        
    }
}
