using System;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public StageController stageController;
    public CinemachineController cinemachineController;
    public ScreenEffectControllder screenEffectControllder;
    
    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
        cinemachineController = FindObjectOfType<CinemachineController>();
        screenEffectControllder = FindObjectOfType<ScreenEffectControllder>();
    }

    private void Start()
    {
        stageController.StartFirstStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stageController.CheckSwitch();
        }
    }
}
