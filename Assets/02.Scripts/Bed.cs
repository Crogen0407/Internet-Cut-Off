using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private Interaction _interaction;
    private ScreenEffectController _screenEffectController;
    private StageController _stageController;
    
    private void Start()
    {
        _screenEffectController = GameManager.Instance.screenEffectController;
        _stageController = GameManager.Instance.stageController;
        _interaction = GetComponent<Interaction>();
        _interaction.action += () =>
        {
            GameManager.Instance.player.isCutScene = true;
            _screenEffectController.Fade("_Brightness", 2, 3, () =>
            {
                _screenEffectController.Fade("_Brightness", 3, 3, () =>
                {
                    GameManager.Instance.player.isCutScene = false;
                    _stageController.sleepAtRealworld = true;
                });
            });
        };
    }
}
