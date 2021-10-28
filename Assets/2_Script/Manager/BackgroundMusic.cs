using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource backAudioSource = null;

    [SerializeField]
    private AudioClip[] audioClips = null;

    private void Start()
    {
        AudioChange();
    }

    public void AudioChange()
    {
        backAudioSource.clip = audioClips[GameManager.Instance.index - 1];
        backAudioSource.Stop();
        backAudioSource.PlayOneShot(backAudioSource.clip);
    }
}