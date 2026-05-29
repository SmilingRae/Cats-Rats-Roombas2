using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        if (PlayerMovement.Instance != null)
        {
            PlayerMovement.Instance.transform.position = playerSpawnPoint.position;

            if (virtualCamera != null)
            {
                virtualCamera.Follow = PlayerMovement.Instance.transform;
            }
        }
    }
}