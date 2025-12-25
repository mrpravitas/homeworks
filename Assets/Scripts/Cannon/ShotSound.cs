using UnityEngine;

public class ShotSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlayShotSound()
    {
        _audioSource?.Play();
    }
}
