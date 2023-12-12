using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StageController : MonoSingleton<StageController>
{
    private int _realworldCountIndex = 0;
    [SerializeField] private int _currentStage;
    [SerializeField] private GameObject _currentStageGameObject;
    public GameObject realWorldStage;
    [SerializeField] private List<StageData> stage;
    private bool _onRealWorld;
    public bool sleepAtRealworld;
    
    
    //Managemets
    private GameManager _gameManager;
    
    //Controllers
    private CinemachineController _cinemachineController;
    private ScreenEffectController _screenEffectController;
    private VolumeController _volumeController;
    
    public bool OnRealWorld
    {
        get => _onRealWorld;
        set
        {
            _onRealWorld = value;
            realWorldStage.SetActive(_onRealWorld);
            _screenEffectController.Fade("_Brightness", 0, 2, () =>
            {
                _gameManager.Player.isCutScene = false;
            });
            if (_onRealWorld == true)
            {
                _volumeController.SetSaturation(_realworldCountIndex);
                _gameManager.Player.transform.position = realWorldStage.transform.Find("SpawnPoint").position;
                _cinemachineController.SetCinemachineConfinerBoundingShape(realWorldStage.transform.Find("BoundShape").GetComponent<PolygonCollider2D>());
                _realworldCountIndex -= 20;
               _gameManager.Player.transform.Find("w").gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            else
            {
                _volumeController.SetSaturation(0);
                _gameManager.Player.transform.Find("w").gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
    
    private void Awake()
    {
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachineController = _gameManager.CinemachineController;
        _screenEffectController = _gameManager.ScreenEffectController;
        _volumeController = _gameManager.VolumeController;
    }

    public void ResetStage(int value)
    {
        _gameManager.Player.isCutScene = true;
        _screenEffectController.Fade("_Brightness", 0, 1, () =>
        {
            _gameManager.Player.isCutScene = false;
        });
         if (_currentStageGameObject != null)
        {
            Destroy(_currentStageGameObject);
        }

        OnRealWorld = false;
        _currentStageGameObject = Instantiate(stage[value].stagePrefab, Vector3.zero, Quaternion.identity);
        _gameManager.Player.transform.position = _currentStageGameObject.transform.Find("SpawnPoint").position;
        PolygonCollider2D boundShape = _currentStageGameObject.transform.Find("BoundShape").GetComponent<PolygonCollider2D>();
        if (boundShape.isTrigger == false)
        {
            boundShape.isTrigger = true;
        }
        _cinemachineController.SetCinemachineConfinerBoundingShape(boundShape);
        SwitchTrigger[] switchTriggers = _currentStageGameObject.transform.Find("Objects").GetComponentsInChildren<SwitchTrigger>();
        int index = 0;
        stage[value].switchTriggers.Clear();
        foreach (var item in switchTriggers)
        {
            stage[value].switchTriggers.Add(item);
            index++;
        }
        _gameManager.Player.unitMaterial.SetFloat("_Noise", 0);
    }

    public void RestartStage()
    {
    }
    
    public int CurrentStage
    {
        get => _currentStage;
        set
        {
            if (stage[_currentStage].isChangeToRealWorld == true)
            {
                _screenEffectController.Fade("_Brightness", 0, 2, () =>
                {
                    _gameManager.Player.isCutScene = true;
                    OnRealWorld = true;
                    Destroy(_currentStageGameObject);
                });
            }
            else if (_currentStage < value)
            {
                if (_currentStage + 1 == stage.Count)
                {
                    _screenEffectController.Fade("_Brightness", 0, 1, () =>
                    {
                        SceneManager.LoadScene("EndingScene_PlayerChoice");
                    });
                }
                else
                {
                    _screenEffectController.Fade("_Brightness", 0, 1, () =>
                    {
                        ResetStage(value);
                    });
                }
            }
            _currentStage = value;
        }
    }
    
    [ContextMenu("GoToInternetWorld")]
    public void GoToInternetWorld()
    {
        if (_onRealWorld == true)
        {
            _screenEffectController.Fade("_Brightness", 0, 1, () =>
            {
                ResetStage(_currentStage);
                _gameManager.Player.isCutScene = false;
                OnRealWorld = false;
            });
            sleepAtRealworld = false;
        }
    }
    
    public void CheckSwitch()
    {
        foreach (SwitchTrigger switchTrigger in stage[CurrentStage].switchTriggers)
        {
            if (switchTrigger.switchOperation == false) return;
        }
        NextStage();
    }
    
    public void StartFirstStage()
    {
        OnRealWorld = true;
    }
    
    /// <summary>
    /// 반환값이 다음 스테이지의 인덱스입니다.
    /// </summary>
    public int NextStage()
    {
        CurrentStage++;
        return CurrentStage;
    }

    /// <summary>
    /// 반환값이 바꾼 스테이지의 인덱스입니다.
    /// </summary>
    public int ChangeStage(int stageCount)
    {
        CurrentStage = stageCount;
        return CurrentStage;
    }
}
