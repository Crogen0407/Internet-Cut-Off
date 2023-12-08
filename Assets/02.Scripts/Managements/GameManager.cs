using System;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public StageController stageController;
    private CinemachineController cinemachineController;
    private void Awake()
    {
        stageController = FindObjectOfType<StageController>();
        cinemachineController = FindObjectOfType<CinemachineController>();
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
