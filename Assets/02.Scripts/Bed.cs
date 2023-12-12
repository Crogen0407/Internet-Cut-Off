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
            if (_stageController.sleepAtRealworld == false)
            {
                GameManager.Instance.Player.isCutScene = true;
                _screenEffectController.Fade("_Brightness", 1, 1, () =>
                {
                    _screenEffectController.Fade("_Brightness", 1, 1, () =>
                    {
                        GameManager.Instance.Player.isCutScene = false;
                        _stageController.sleepAtRealworld = true;
                    });
                });
            }
            else
            {
                GameManager.Instance.MessageController.DrawMessage("지금은 피곤하지 않다.", () =>
                {
                    GameManager.Instance.MessageController.DrawMessage(" ");
                });
            }
        };
    }
}
