using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoState<T> : MonoBehaviour
    where T : MonoBehaviour
{
    static MonoState<T> _instance;

    protected static object _lock = new object();

    protected virtual void Awake()
    {
        bool destroyMe = true;

        if(_instance == null)
        {
            lock(_lock)
            {
                if(_instance == null)
                {
                    destroyMe = false;
                    _instance = this;
                }
            }
        }

        if (destroyMe)
        {
            Destroy(gameObject);
            return;
        }
    }

    public static T Instance
    {
        get
        {
            return _instance as T;
        }
    }
}
