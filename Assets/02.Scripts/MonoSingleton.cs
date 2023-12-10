using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance = null;

    delegate int gg(int a, int b);

    private void Start()
    {
        gg DD = delegate(int i, int i1)
        {
            return 1;
        };

        DD?.Invoke(1, 2);
    }


    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }
            else
            {
                Destroy(instance.gameObject);
            }
            return instance;
        }
    }
}
