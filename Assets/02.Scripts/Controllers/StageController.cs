using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    [SerializeField] private int _currentStage;
    [SerializeField] private GameObject _currentStageGameObject;
    [SerializeField] private List<StageData> stage;
    
    public int CurrentStage
    {
        get => _currentStage;
        set
        {
            if (_currentStage < value)
            {
                Destroy(_currentStageGameObject);
                _currentStageGameObject = Instantiate(stage[value].stagePrefab, Vector3.zero, Quaternion.identity);
            }
            _currentStage = value;
        }
    }

    public List<StageData> Stage
    {
        get => stage;
        set
        {
            stage = value;
            
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
        _currentStageGameObject = Instantiate(stage[0].stagePrefab, Vector3.zero, Quaternion.identity);
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
