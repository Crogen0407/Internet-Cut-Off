using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private Interaction _interaction;
    private ScreenEffectController _screenEffectController;
    private StageController _stageController;
    
    private void Start()
    {
        _stageController = GameManager.Instance.stageController;
        _screenEffectController = GameManager.Instance.screenEffectController;
        _interaction = GetComponent<Interaction>();
        
        _interaction.action += () =>
        {
            if (_stageController.sleepAtRealworld == true)
            {
                GameManager.Instance.player.isCutScene = true;
                _stageController.GoToInternetWorld();
            }
        };
    }
}
