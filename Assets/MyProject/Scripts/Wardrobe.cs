using System;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [SerializeField] private AudioClip _doorOpenSound;
    [SerializeField] private AudioClip _doorCloseSound;
    [SerializeField] private SoundPlayer _soundPlayer;

    public event Action<bool> IsDoorOpened;
    public event Action DoorOpened;

    public void InteractWithDoor(bool isOpening)
    {
        IsDoorOpened.Invoke(isOpening);

        if (isOpening)
            _soundPlayer.PlaySound(_doorOpenSound);

        if (_soundPlayer.Source.isPlaying && isOpening == false)
            _soundPlayer.PlaySound(_doorCloseSound);
    }

    public void OpenDoor() => 
        DoorOpened?.Invoke();
}
