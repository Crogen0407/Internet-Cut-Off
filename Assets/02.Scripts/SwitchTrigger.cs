using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : MonoBehaviour
{
    public bool switchOperation;
    [SerializeField] private Sprite switchCompleteSprite;
    
    //Components
    private Interaction _interaction;
    
    private void Awake()
    {
        _interaction = GetComponent<Interaction>();
    }

    private void Start()
    {
        _interaction.action += () =>
        {
            switchOperation = true;
            GameManager.Instance.stageController.CheckSwitch();
            gameObject.SetActive(false);
        };
    }
}
