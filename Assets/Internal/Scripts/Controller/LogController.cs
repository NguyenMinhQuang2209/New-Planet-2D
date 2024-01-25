using System;
using UnityEditor.VersionControl;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public static LogController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Log(string message)
    {
        Debug.Log(message);
    }
    public void Log(Exception e)
    {
        Debug.Log(e.Message);
    }
}
