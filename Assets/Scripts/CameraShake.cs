using UnityEngine;
using Cinemachine;
using System.Collections;

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

    public void HitStop(float duration)
    {
        Time.timeScale = 0f;

        StartCoroutine(HitStopRoutine(duration));
    }

    private IEnumerator HitStopRoutine(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = 1f;
    }
}