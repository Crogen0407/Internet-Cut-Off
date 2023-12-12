using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoSingleton<GameManager>
{
    //Controller
    public StageController StageController { get; private set; }
    public CinemachineController CinemachineController { get; private set; }
    public ScreenEffectController ScreenEffectController { get; private set; }
    public VolumeController VolumeController { get; private set; }
    public MessageController MessageController { get; private set; }
    public Player Player { get; private set; }
    
    private void Awake()
    {
        StageController = FindObjectOfType<StageController>();
        CinemachineController = FindObjectOfType<CinemachineController>();
        ScreenEffectController = FindObjectOfType<ScreenEffectController>();
        VolumeController = FindObjectOfType<VolumeController>();
        MessageController = FindObjectOfType<MessageController>();
        
        Player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        if (StageController != null)
        {
            StageController.StartFirstStage();
        }
    }

    private void Update()
    {
        
    }
}
