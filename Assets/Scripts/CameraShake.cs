using UnityEngine;
using Cinemachine; 

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

   
    public void Shake(float magnitude)
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(magnitude);
        }
    }
}