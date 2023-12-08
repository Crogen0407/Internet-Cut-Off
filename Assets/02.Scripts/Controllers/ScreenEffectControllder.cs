using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScreenEffectControllder : MonoBehaviour
{
    [SerializeField] private Material _screenEffect;

    private void Awake()
    {
        _screenEffect.SetFloat("_Brightness", 0);

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

    public void BrightnessFade(float waitTime, float duration)
    {
        StartCoroutine(BrightnessFadeCoroutine(waitTime, duration));
    }

    private IEnumerator BrightnessFadeCoroutine(float waitTime, float duration)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float currentBrightness = _screenEffect.GetFloat("_Brightness");
        Debug.Log(currentBrightness);
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentBrightness < 0.5f ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            if (fadeMode == true)
            {
                _screenEffect.SetFloat("_Brightness", percentTime);
            }
            else
            {
                _screenEffect.SetFloat("_Brightness", 1 - percentTime);
            }
            yield return null;
        }
    }
    
    public void BrightnessFade(float waitTime, float duration, Action lateAction)
    {
        StartCoroutine(BrightnessFadeCoroutine(waitTime, duration, lateAction));
    }

    private IEnumerator BrightnessFadeCoroutine(float waitTime, float duration, Action lateAction)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        float currentBrightness = _screenEffect.GetFloat("_Brightness");
        Debug.Log(currentBrightness);
        float currentTime = 0;
        float percentTime = 0;
        bool fadeMode = currentBrightness < 0.5f ? true : false;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            if (fadeMode == true)
            {
                _screenEffect.SetFloat("_Brightness", percentTime);
            }
            else
            {
                _screenEffect.SetFloat("_Brightness", 1 - percentTime);
            }
            yield return null;
        }
        lateAction?.Invoke();
    }
}