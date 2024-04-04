
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else if (soundIndex >= 1 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else if (soundIndex >= 2 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else if (soundIndex >= 3 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else if (soundIndex >= 4 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else if (soundIndex >= 5 && soundIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[soundIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid sound index");
        }
    }
}
