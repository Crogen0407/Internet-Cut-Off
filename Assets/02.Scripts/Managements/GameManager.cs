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
    
    [HideInInspector] public Player player;
    
    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
        cinemachineController = FindObjectOfType<CinemachineController>();
        screenEffectController = FindObjectOfType<ScreenEffectController>();

        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Debug.Log("dkdk");
        stageController.StartFirstStage();
        screenEffectController.SetScreenEffect("_LatterboxCurrentSize", 0);
    }

    private void Update()
    {
        
    }
}
