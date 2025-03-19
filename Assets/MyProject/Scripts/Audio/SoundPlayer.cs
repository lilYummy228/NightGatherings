using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public AudioSource Source => _source;

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            if (_source.isPlaying)
                _source.Stop();

            _source.clip = audioClip;

            _source.Play();
        }
    }
}
