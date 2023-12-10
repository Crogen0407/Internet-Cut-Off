using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/SoundVolumeData", fileName = "SoundVolumeData")]
public class SO_SoundVolumeData : ScriptableObject
{
    public Transform UI_Settings { get; private set; }
    
    private Slider _msSoundVolumeText;
    private Slider _sfSoundVolumeText;
    private Slider _bgmSoundVolumeText;
    
    [SerializeField] private int _msSoundVolume = 0;
    [SerializeField] private int _sfSoundVolume = 0;
    [SerializeField] private int _bgmSoundVolume = 0;

    public void Init()
    {
        UI_Settings = GameObject.Find("UI_Settings").transform;
        
        _msSoundVolumeText = UI_Settings.Find("MSSoundVolume/Volume").GetComponent<Slider>();
        _sfSoundVolumeText = UI_Settings.Find("SFSoundVolume/Volume").GetComponent<Slider>();
        _bgmSoundVolumeText = UI_Settings.Find("BGMSoundVolume/Volume").GetComponent<Slider>();
    }

    //0~10
    public int MSSoundVolume
    {
        get => _msSoundVolume;
        set
        {
            _msSoundVolume = value;
            _msSoundVolume = Mathf.Clamp(_msSoundVolume, 0, 10);     
        }
    }
    
    //0~10
    public int SFSoundVolume
    {
        get => _sfSoundVolume;
        set
        {
            _sfSoundVolume = value;
            _sfSoundVolume = Mathf.Clamp(_sfSoundVolume, 0, 10);     
        }
    }

    //0~10
    public int BGMSoundVolume
    {
        get => _bgmSoundVolume;
        set
        {
            _bgmSoundVolume = value;
            _bgmSoundVolume = Mathf.Clamp(_bgmSoundVolume, 0, 10);     
        }
    }
}