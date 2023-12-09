using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    //Managements
    private TimelineManager _timelineManager;
    private ScreenEffectController _screenEffectController;
    
    private Player _player;

    private void Start()
    {
        _timelineManager = TimelineManager.Instance;
        _screenEffectController = _timelineManager.screenEffectController;
        _player = _timelineManager.player;
    }

    public void Fade(string type)
    {
        _screenEffectController.Fade(type, 0, 2);
    }

    public void BrightnessFade(float duration)
    {
        _screenEffectController.Fade("_Brightness", 0, duration);
    }
    
    public void SetPlayerFace(float direction)
    {
        _player.Face = (sbyte)direction;
    }
    
    public void PlayerMove(float direction)
    {
        _player.developmentVelocity = new Vector3(direction, 0);
    }
    
    public void SetPlayerPosition(Transform transformPos)
    {
        _player.transform.position = transformPos.position;
    }
}