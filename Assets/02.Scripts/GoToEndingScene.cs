using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndingScene : MonoBehaviour
{
    [SerializeField] private EndingType _endingType;
    [SerializeField] private GameObject doorUI;
    private Interaction _interaction;
    private VolumeController _volumeController;
    private ScreenEffectController _screenEffectController;
    private Player _player;
    
    private void Start()
    {
        _interaction = GetComponent<Interaction>();
        _volumeController = PlayerChoiceManager.Instance.volumeController;
        _screenEffectController = PlayerChoiceManager.Instance.screenEffectController;
        _player = PlayerChoiceManager.Instance.player;
        
        _interaction.action += () =>
        {
            _player.isCutScene = true;
            switch (_endingType)
            {
                case EndingType.Real:
                    _volumeController.SetRealEndingBounding(7, () =>
                    {
                        _player.isCutScene = false;
                        SceneManager.LoadScene("EndingScene_Real");
                    });
                    break;
                case EndingType.Internet:
                    _screenEffectController.Fade("_Brightness", 0, 3, () =>
                    {
                        _player.isCutScene = false;
                        SceneManager.LoadScene("EndingScene_Internet");
                    });
                    break;
            }
        };
    }
}
