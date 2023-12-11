using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScreenEffectController : MonoBehaviour
{
    [SerializeField] private Material _screenEffect;

    private void Awake()
    {
        SetScreenEffect("_Brightness", 0);
    }

    public void SetScreenEffect<T>(string type, T value) where T : struct
    {
        if (value is float)
        {
            _screenEffect.SetFloat(type, float.Parse(value.ToString()));
        }
        else if (value is int)
        {
            _screenEffect.SetInt(type, int.Parse(value.ToString()));
        }
        else if (value is bool)
        {
            _screenEffect.SetInt(type, Convert.ToInt32(value));
        }
    }

    public void Fade(string type, float waitTime, float duration)
    {
        StartCoroutine(FadeCoroutine(type, waitTime, duration));
    }

    private IEnumerator FadeCoroutine(string type, float waitTime, float duration)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float currentBrightness = _screenEffect.GetFloat(type);
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentBrightness < 0.5f ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            if (fadeMode == true)
            {
                _screenEffect.SetFloat(type, percentTime);
            }
            else
            {
                _screenEffect.SetFloat(type, 1 - percentTime);
            }
            yield return null;
        }
    }
    
    public void Fade(string type, float waitTime, float duration, Action lateAction)
    {
        StartCoroutine(FadeCoroutine(type, waitTime, duration, lateAction));
    }

    private IEnumerator FadeCoroutine(string type, float waitTime, float duration, Action lateAction)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float currentBrightness = _screenEffect.GetFloat(type);
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentBrightness < 0.5f ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            if (fadeMode == true)
            {
                _screenEffect.SetFloat(type, percentTime);
            }
            else
            {
                _screenEffect.SetFloat(type, 1 - percentTime);
            }
            yield return null;
        }

        yield return new WaitForSeconds(duration);
        lateAction?.Invoke();
    }
}