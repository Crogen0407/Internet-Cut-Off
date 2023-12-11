using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceManager : MonoSingleton<PlayerChoiceManager>
{
    [HideInInspector] public ScreenEffectController screenEffectController;
    [HideInInspector] public Player player;
    [HideInInspector] public VolumeController volumeController;
    
    private void Awake()
    {
        screenEffectController = FindObjectOfType<ScreenEffectController>();
        volumeController = FindObjectOfType<VolumeController>();
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        player.isCutScene = true;
        screenEffectController.Fade("_Brightness", 0, 1, () =>
        {
            player.isCutScene = false;
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
