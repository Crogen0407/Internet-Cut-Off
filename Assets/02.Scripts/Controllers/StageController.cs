using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoSingleton<StageController>
{
    [SerializeField] private int _currentStage;
    [SerializeField] private List<StageData> stage;
    
    public int CurrentStage
    {
        get => _currentStage;
        set
        {
            _currentStage = value;
        }
    }

    public List<StageData> Stage
    {
        get => stage;
        set
        {
            stage = value;
            if (stage[_currentStage].yellowEnemyCount <= 0)
            {
                NextStage();
            }
            else if (stage[_currentStage].blueEnemyCount <= 0)
            {
                
            }
            else if (stage[_currentStage].redEnemyCount <= 0)
            {
                
            }
        }
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
