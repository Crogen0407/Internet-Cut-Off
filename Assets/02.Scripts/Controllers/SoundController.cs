using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private SO_SoundVolumeData _soundVolumeData;
    [SerializeField] private AudioMixer _audioMixer;

    public Transform UI_Settings { get; private set; }


    private Slider _msSoundVolumeSlider;
    private Slider _sfSoundVolumeSlider;
    private Slider _bgmSoundVolumeSlider;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        UI_Settings = FindObjectOfType<Canvas>().transform.Find("EscapePanel").Find("UI_Settings");
        _msSoundVolumeSlider = UI_Settings.Find("MSSoundVolume/Volume").GetComponent<Slider>();
        _bgmSoundVolumeSlider = UI_Settings.Find("BGMSoundVolume/Volume").GetComponent<Slider>();
        _sfSoundVolumeSlider = UI_Settings.Find("SFSoundVolume/Volume").GetComponent<Slider>();
        
        _msSoundVolumeSlider.value = _soundVolumeData.MSSoundVolume;
        _bgmSoundVolumeSlider.value = _soundVolumeData.BGMSoundVolume;
        _sfSoundVolumeSlider.value = _soundVolumeData.SFSoundVolume;
    }

    public void OnEnable()
    {
        try
        {
            _msSoundVolumeSlider.value = _soundVolumeData.MSSoundVolume;
            _bgmSoundVolumeSlider.value = _soundVolumeData.BGMSoundVolume;
            _sfSoundVolumeSlider.value = _soundVolumeData.SFSoundVolume;
        }
        catch (NullReferenceException e){}
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void ChangeClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
    
    public void LoadSoundVolume()
    {
        _audioMixer.SetFloat("Master", Mathf.Log10((_soundVolumeData.MSSoundVolume == 0 ? 0.0001f : _soundVolumeData.MSSoundVolume)/10f) * 20);
        _audioMixer.SetFloat("BGM", Mathf.Log10((_soundVolumeData.BGMSoundVolume == 0 ? 0.0001f : _soundVolumeData.BGMSoundVolume)/10f) * 20);
        _audioMixer.SetFloat("SFX", Mathf.Log10((_soundVolumeData.SFSoundVolume == 0 ? 0.0001f : _soundVolumeData.SFSoundVolume)/10f) * 20);
    }

    public void SetMasterVolume(float value)
    {
        _soundVolumeData.MSSoundVolume = (int)value;
        LoadSoundVolume();
    }
    
    public void SetBGMVolume(float value)
    {
        _soundVolumeData.BGMSoundVolume = (int)value;
        LoadSoundVolume();
    }
    
    public void SetSFXVolume(float value)
    {
        _soundVolumeData.SFSoundVolume = (int)value;
        LoadSoundVolume();
    }
}
