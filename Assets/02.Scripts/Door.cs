using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Interaction _interaction;
    private StageController _stageController;
    private AudioSource _audioSource;

    private void Awake()
    {
        _interaction = GetComponent<Interaction>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    private void Start()
    {
        _interaction.action += () =>
        {
            _audioSource.Play();
            GameManager.Instance.MessageController.DrawMessage("나갈 필요가 없다.", () =>
            {
                GameManager.Instance.MessageController.DrawMessage(" ");
            });
        };
    }
}
