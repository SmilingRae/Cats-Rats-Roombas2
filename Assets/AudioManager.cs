using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("--- Audio Source---")]
    [SerializeField] AudioSource musicSource;
      [SerializeField] AudioSource SFXSource;

      [Header ("--- Audio Clip---")]
      public AudioClip background;
      public AudioClip death;
      public AudioClip attack;
      public AudioClip pickup;

      private void Start()
      {

      }

      public void PlaySFX(AudioClip clip)
      {
          SFXSource.PlayOneShot(clip);
      }
}
