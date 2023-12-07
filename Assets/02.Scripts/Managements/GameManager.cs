using System;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private StageController _stageController;
    
    private void Awake()
    {
        _stageController = FindObjectOfType<StageController>();
    }

    private void Start()
    {
        _stageController.StartFirstStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _stageController.CheckSwitch();
        }
    }
}
