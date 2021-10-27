using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip[] audioClips = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AudioChange()
    {
        if (GameManager.Instance.index == 1)
            audioSource.clip = audioClips[0];
        else if (GameManager.Instance.index == 2)
            audioSource.clip = audioClips[1];
        else
            audioSource.clip = audioClips[2];
        audioSource.Play();
    }
}