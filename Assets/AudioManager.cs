using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("--- Audio Source---")]
    [SerializeField] AudioSource musicSource;
      [SerializeField] AudioSource SFXSource;

      [Header ("--- Audio Clip---")]
      public AudioClip enemyDamage;
      public AudioClip mouseAttack;
      public AudioClip attack;
      public AudioClip pickup;
      public AudioClip dogAttack;
      public AudioClip playerDamage;

      private void Start()
      {

      }

      public void PlaySFX(AudioClip clip)
      {
          SFXSource.PlayOneShot(clip);
      }
}
