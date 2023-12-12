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
        _screenEffectController = GameManager.Instance.ScreenEffectController;
        _stageController = GameManager.Instance.StageController;
        _interaction = GetComponent<Interaction>();
        _interaction.action += () =>
        {
            GameManager.Instance.Player.isCutScene = true;
            _screenEffectController.Fade("_Brightness", 2, 3, () =>
            {
                _screenEffectController.Fade("_Brightness", 3, 3, () =>
                {
                    GameManager.Instance.Player.isCutScene = false;
                    _stageController.sleepAtRealworld = true;
                });
            });
        };
    }
}
