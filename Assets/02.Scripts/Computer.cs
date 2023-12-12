using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    private Interaction _interaction;
    private ScreenEffectController _screenEffectController;
    private StageController _stageController;
    private MessageController _messageController;
    
    private void Start()
    {
        _stageController = GameManager.Instance.StageController;
        _screenEffectController = GameManager.Instance.ScreenEffectController;
        _interaction = GetComponent<Interaction>();
        
        _interaction.action += () =>
        {
            if (_stageController.sleepAtRealworld == true)
            {
                GameManager.Instance.Player.isCutScene = true;
                _stageController.GoToInternetWorld();
            }
            else
            {
                GameManager.Instance.MessageController.DrawMessage("너무 피곤하다. 휴식을 취하는 게 좋을 것 같다.", () =>
                {
                    GameManager.Instance.MessageController.DrawMessage(" ");
                });
            }
        };
    }
}
