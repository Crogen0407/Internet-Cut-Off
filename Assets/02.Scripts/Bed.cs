using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private Interaction _interaction;
    private ScreenEffectController _screenEffectController;
    
    private void Start()
    {
        _screenEffectController = GameManager.Instance.screenEffectController;
        _interaction = GetComponent<Interaction>();
        _interaction.action += () =>
        {
            GetComponent<Collider2D>().enabled = false;
            _screenEffectController.Fade("_Brightness", 2, 3, () =>
            {
                _screenEffectController.Fade("_Brightness", 3, 3, () =>
                {
                    GetComponent<Collider2D>().enabled = true;
                });
            });
        };
    }
}
