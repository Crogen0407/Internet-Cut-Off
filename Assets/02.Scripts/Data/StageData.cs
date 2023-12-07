using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    public GameObject stagePrefab;
    [Space] [Header("SwitchTrigger")] 
    public List<SwitchTrigger> switchTriggers;
}
