using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    private static bool shuttingDown = false;
    private static object locker = new object();

    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance " + typeof(T) + " is already destroyed. returning null");
                return null;
            }

            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                        DontDestroyOnLoad(instance);
                    }
                }
                return instance;
            }
        }
    }

    private void OnDestroy()
    {
        shuttingDown = true;
    }

    private void OnApplicationQuit()
    {
        shuttingDown = true;
    }
}
