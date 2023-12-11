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
            if (_onRealWorld == true)
            {
                _volumeController.SetSaturation(_realworldCountIndex);
                _gameManager.player.transform.position = realWorldStage.transform.Find("SpawnPoint").position;
                _realworldCountIndex -= 20;
            }
            else
            {
                _volumeController.SetSaturation(0);
            }
        }
    }
    
    private void Awake()
    {
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachineController = _gameManager.cinemachineController;
        _screenEffectController = _gameManager.screenEffectController;
        _volumeController = _gameManager.volumeController;
    }

    public void ResetStage(int value)
    {
        _screenEffectController.Fade("_Brightness", 0, 1);
         if (_currentStageGameObject != null)
        {
            Destroy(_currentStageGameObject);
        }

        OnRealWorld = false;
        _currentStageGameObject = Instantiate(stage[value].stagePrefab, Vector3.zero, Quaternion.identity);
        _gameManager.player.transform.position = _currentStageGameObject.transform.Find("SpawnPoint").position;
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
        _gameManager.player.unitMaterial.SetFloat("_Noise", 0);
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
                Destroy(_currentStageGameObject);
                OnRealWorld = true;
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
                _gameManager.player.isCutScene = false;
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
        Debug.Log("dkldk");
        ResetStage(0);
        OnRealWorld = false;
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
