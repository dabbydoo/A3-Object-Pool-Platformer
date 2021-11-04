using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T GetInstance()
    {
        if(instance == null)
        {
            instance = FindObjectOfType<T>();
            if(instance == null)
            {
                GameObject gO = new GameObject();
                gO.name = typeof(T).Name;
                instance = gO.AddComponent<T>();
            }
        }
        return instance;
    }

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this as T)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
