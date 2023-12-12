using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    private Volume _realWorldVolume;
    private ColorAdjustments _colorAdjustments;
    private Bloom _bloom;
    
    private void Start()
    {
        try
        {
            _realWorldVolume = FindObjectOfType<GameManager>().StageController.realWorldStage.transform.Find("Global Volume")
                .GetComponent<Volume>();
        }
        catch (NullReferenceException e)
        {
            _realWorldVolume = GameObject.Find("RealWorld").transform.Find("Global Volume").GetComponent<Volume>();

        }
        _realWorldVolume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
        _realWorldVolume.profile.TryGet<Bloom>(out _bloom);
    }

    public void SetSaturation(float value)
    {
        _colorAdjustments.saturation.value = value;
    }

    public void SetBloom(float value)
    {
        _bloom.intensity.value = value;
    }

    public void SetRealEndingBounding(float duration, Action action)
    {
        StartCoroutine(SetRealEndingBoundingCoroutine(duration, action));
    }

    private IEnumerator SetRealEndingBoundingCoroutine(float duration, Action action)
    {
        float currentBloom = _bloom.intensity.value;
        float currentTime = 0;
        float percentTime = 0;
        while (percentTime < 1)
        {
            currentTime += Time.deltaTime;
            percentTime = currentTime / duration;
            _bloom.intensity.value = Mathf.Lerp(currentBloom, 80000, easeInCubic(percentTime));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(duration);
        action?.Invoke();
    }
    
    float easeInCubic(float x)
    {
        return x * x * x;
    }
}
