using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Singleton Settings")]
    public bool dontDestroyOnLoad;
    public bool destroyDuplicate;

    public static T Instance => instance;
    private static T instance;

    public virtual void Awake()
    {
        instance = gameObject.GetComponent<T>();
        if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (destroyDuplicate && Instance != this) Destroy(gameObject);
    }

    public virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (destroyDuplicate && Instance != this) Destroy(gameObject);
    }

    public virtual void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
}