using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneController : MonoBehaviour
{
    private RectTransform latterbox_up;
    private RectTransform latterbox_bottom;
    
    //Managements
    private TimelineManager _timelineManager;
    private ScreenEffectController _screenEffectController;
    
    private Player _player;

    private void Start()
    {
        _timelineManager = TimelineManager.Instance;

        latterbox_up = _timelineManager.canvasTransform.Find("Latterbox_up").GetComponent<RectTransform>();
        latterbox_bottom = _timelineManager.canvasTransform.Find("Latterbox_bottom").GetComponent<RectTransform>();
        
        _screenEffectController = _timelineManager.screenEffectController;
        _player = _timelineManager.player;
    }

    public void Fade(string type)
    {
        _screenEffectController.Fade(type, 0, 2);
    }

    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LatterboxFade(float duration)
    {
        StartCoroutine(LatterboxFadeCoroutine(duration));
    }
    
    private IEnumerator LatterboxFadeCoroutine(float duration)
    {
        float currentBrightness = latterbox_up.localScale.y;
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentBrightness < 0.5f ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            Vector3 scaleVec;
            if (fadeMode == true)
            {
                scaleVec = new Vector3(1, percentTime, 1);
                latterbox_up.localScale = scaleVec;
                latterbox_bottom.localScale = scaleVec;
            }
            else
            {
                scaleVec = new Vector3(1, 1 - percentTime, 1);
                latterbox_up.localScale = scaleVec;
                latterbox_bottom.localScale = scaleVec;            }
            yield return null;
        }
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

    public void SetBlackAndWhite(bool parameter)
    {
        _screenEffectController.SetScreenEffect("_BlackAndWhite", parameter);
    }
}