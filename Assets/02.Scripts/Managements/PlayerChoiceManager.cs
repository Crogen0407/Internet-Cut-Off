using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceManager : MonoBehaviour
{
    public ScreenEffectController screenEffectController;
    public Player player;
    
    private void Awake()
    {
        screenEffectController = FindObjectOfType<ScreenEffectController>();
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
