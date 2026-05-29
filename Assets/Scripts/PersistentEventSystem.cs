using UnityEngine;
using UnityEngine.EventSystems;

public class PersistentEventSystem : MonoBehaviour
{
    public static PersistentEventSystem Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // Destroy the OLD one, keep the new scene one
            Destroy(Instance.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}