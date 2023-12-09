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
        _screenEffectController.Fade(type, 2, 2);
    }

    public void SetPlayerFace(float direction)
    {
        _player.Face = (sbyte)direction;
    }
}