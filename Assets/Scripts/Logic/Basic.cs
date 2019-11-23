using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleston<T> where T : Singleston<T>, new()
{
    public static T instance
    {
        get
        {
            if(null == s_Instance)
            {
                s_Instance = new T();
            }
            return s_Instance;
        }
    }

    private static T s_Instance;
}
