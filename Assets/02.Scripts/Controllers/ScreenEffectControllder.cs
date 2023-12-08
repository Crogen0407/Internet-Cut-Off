using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScreenEffectControllder : MonoBehaviour
{
    [SerializeField] private Material _screenEffect;

    public void SetScreenEffect<T>(string type, T value) where T : struct
    {
        if (value is float)
        {
            _screenEffect.SetFloat(type, float.Parse(value.ToString()));
        }
        else if (value is int)
        {
            _screenEffect.SetInt(type, int.Parse(value.ToString()));
        }
        else if (value is bool)
        {
            _screenEffect.SetInt(type, Convert.ToInt32(value));
        }
    }
}