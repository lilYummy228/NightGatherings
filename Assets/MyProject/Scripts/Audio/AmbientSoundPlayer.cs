using System.Collections;
using UnityEngine;

public class AmbientSoundPlayer : SoundPlayer
{
    [SerializeField] private float _minDelay = 6f;
    [SerializeField] private float _maxDelay = 12f;
    [SerializeField] private AudioClip[] _clips;

    private WaitWhile _waitWhile => new(() => Source.isPlaying);

    private void Start() =>
        StartCoroutine(PlayAmbientSound());

    private IEnumerator PlayAmbientSound()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));

            AudioClip randomClip = _clips[Random.Range(0, _clips.Length - 1)];            

            PlaySound(randomClip);

            yield return _waitWhile;
        }
    }
}
