using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    [SerializeField] private int _currentStage;
    [SerializeField] private GameObject _currentStageGameObject;
    private GameObject _realWorldStage;
    [SerializeField] private List<StageData> stage;
    private bool _onRealWorld;
    
    //Managemets
    private GameManager _gameManager;
    
    //Controllers
    private CinemachineController _cinemachineController;
    private ScreenEffectControllder _screenEffectControllder;
    
    public bool OnRealWorld
    {
        get => _onRealWorld;
        set
        {
            _onRealWorld = value;
            _screenEffectControllder.SetScreenEffect("_BlackAndWhite", _onRealWorld);
            _realWorldStage.SetActive(_onRealWorld);
            _currentStageGameObject.SetActive(!_onRealWorld);
        }
    }
    
    private void Awake()
    {
        _realWorldStage = GameObject.Find("RealWorld");
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _cinemachineController = _gameManager.cinemachineController;
        _screenEffectControllder = _gameManager.screenEffectControllder;
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
                Destroy(_currentStageGameObject);
                _currentStageGameObject = Instantiate(stage[value].stagePrefab, Vector3.zero, Quaternion.identity);
                PolygonCollider2D boundShape = _currentStageGameObject.transform.Find("BoundShape").GetComponent<PolygonCollider2D>();
                _cinemachineController.SetCinemachineConfinerBoundingShape(boundShape);
                SwitchTrigger[] switchTriggers = _currentStageGameObject.transform.Find("Objects").GetComponentsInChildren<SwitchTrigger>();
                int index = 0;
                foreach (var item in switchTriggers)
                {
                    stage[value].switchTriggers.Add(item);
                    index++;
                }
            }
            _currentStage = value;
        }
    }
    
    [ContextMenu("GoToInternetWorld")]
    private void GoToInternetWorld()
    {
        if (_onRealWorld == true)
        {
            _currentStageGameObject = Instantiate(stage[_currentStage].stagePrefab, Vector3.zero, Quaternion.identity);
            OnRealWorld = false;
        }
    }
    
    public void CheckSwitch()
    {
        foreach (SwitchTrigger switchTrigger in stage[_currentStage].switchTriggers)
        {
            if (switchTrigger.switchOperation == false)
            {
                return;
            }
        }
        NextStage();
    }
    
    public void StartFirstStage()
    {
        _screenEffectControllder.SetScreenEffect("_BlackAndWhite", false);
        _currentStageGameObject = Instantiate(stage[0].stagePrefab, Vector3.zero, Quaternion.identity);
        SwitchTrigger[] switchTriggers = _currentStageGameObject.transform.Find("Objects").GetComponentsInChildren<SwitchTrigger>();
        int index = 0;
        foreach (var item in switchTriggers)
        {
            stage[_currentStage].switchTriggers.Add(item);
            index++;
        }
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
