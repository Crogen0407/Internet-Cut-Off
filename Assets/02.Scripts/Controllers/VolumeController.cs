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
    private void Start()
    {
        _realWorldVolume = GameManager.Instance.stageController.realWorldStage.transform.Find("Global Volume")
            .GetComponent<Volume>();
        _realWorldVolume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);
    }

    public void SetSaturation(float value)
    {
        _colorAdjustments.saturation.value = value;
    }
}
