﻿using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    private static object instanceLock = new object();

    public static T Instance
    {
        get
        {
            //if (applicationIsQuitting)
            //    return null;

            lock (instanceLock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        if (!instance.ShouldDestroyOnLoad())
                            DontDestroyOnLoad(singleton);
                    }
                }

                return instance;
            }
        }
    }

    protected virtual bool ShouldDestroyOnLoad()
    {
        return true;
    }

    protected virtual void OnDestroy()
    {
        instance = null;
    }
}