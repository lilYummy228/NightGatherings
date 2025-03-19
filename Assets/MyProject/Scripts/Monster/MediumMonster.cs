using System;
using UnityEngine;

public class MediumMonster : Monster
{
    [SerializeField] private AudioClip _scratchingSound;

    public event Action<bool> IsScratching;

    protected override void OnDoorOpened() =>
        _stateChangerCoroutine = StartCoroutine(base.WaitForChangeState(StayOpened));

    private void StayOpened()
    {
        StopCoroutine(_stateChangerCoroutine);
        StopCoroutine(_bideCoroutine);

        _bideCoroutine = StartCoroutine(Bide(Scratch));

        Debug.Log("Door Opened");
    }

    private void Scratch(bool isScratching)
    {
        IsScratching?.Invoke(isScratching);

        if (isScratching)
            _soundPlayer.PlaySound(_scratchingSound);

        if (_soundPlayer.Source.isPlaying && isScratching == false)
            _soundPlayer.Source.Stop();
    }
}
