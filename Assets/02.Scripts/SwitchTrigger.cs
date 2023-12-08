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
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _interaction = GetComponent<Interaction>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _interaction.action += () =>
        {
            _spriteRenderer.color = Color.green;
            switchOperation = true;
            GameManager.Instance.stageController.CheckSwitch();
        };
    }
}
