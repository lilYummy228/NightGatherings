using System;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] private AudioClip _doorOpenSound;
    [SerializeField] private AudioClip _doorCloseSound;
    [SerializeField] private SoundPlayer _soundPlayer;

    public event Action<bool> IsDoorOpened;
    public event Action OnOpened;

    public void InteractWithDoor(bool isOpened)
    {
        IsDoorOpened.Invoke(isOpened);

        if (_soundPlayer.Source.isPlaying)
            _soundPlayer.Source.Stop();

        if (isOpened)
            _soundPlayer.PlaySound(_doorOpenSound);
        else
            _soundPlayer.PlaySound(_doorCloseSound);
    }

    public void OpenDoor() =>
        OnOpened?.Invoke();
}
