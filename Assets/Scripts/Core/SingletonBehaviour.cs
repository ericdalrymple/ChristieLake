using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SingletonBehaviour<T> : MonoBehaviour
    where T : SingletonBehaviour<T>
{
    private static SingletonBehaviour<T> s_Instance = null;

    public static T Instance
    {
        get
        {
            return (T)s_Instance;
        }
    }

    void OnEnable()
    {
        if (Instance == null)
        {
            s_Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    protected virtual void OnSceneLoaded() { }
    protected virtual void OnSceneUnloaded() { }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnSceneLoaded();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        OnSceneUnloaded();
    }
}
