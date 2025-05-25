using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] protected MonsterSetup _monster;
    [SerializeField] protected Wardrobe _wardrobe;

    protected bool _isChangingState;
    protected WaitUntil _waitUntil;
    protected WaitForSeconds _wait;
    protected Coroutine _stateChangerCoroutine = null;
    protected Coroutine _bideCoroutine;

    public event Action Jumpedscare;

    private void Awake()
    {
        _waitUntil = new WaitUntil(() => _isChangingState == false);
        _wait = new WaitForSeconds(_monster.JumpscareDelay);

        _bideCoroutine = StartCoroutine(Bide(_wardrobe.InteractWithDoor));
    }

    private void OnEnable() =>
        _wardrobe.DoorOpened += OnDoorOpened;

    private void OnDisable() =>
        _wardrobe.DoorOpened -= OnDoorOpened;

    private void OnDoorOpened() =>
        TryToJumpscare();

    public void SetAbleStatus(bool isChangingState) =>
        _isChangingState = isChangingState;

    public void TryToJumpscare() => 
        _stateChangerCoroutine = StartCoroutine(WaitForChangeState(Jumpscare));

    private IEnumerator Bide(Action<bool> action)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_monster.BideMinTime, _monster.BideMaxTime));

            action.Invoke(_isChangingState);

            yield return _waitUntil;

            if (_stateChangerCoroutine != null)
                StopCoroutine(_stateChangerCoroutine);

            action.Invoke(false);
        }
    }

    private IEnumerator WaitForChangeState(Action action)
    {
        yield return _wait;

        action.Invoke();
    }

    private void Jumpscare()
    {
        StopCoroutine(_bideCoroutine);

        Jumpedscare?.Invoke();

        _soundPlayer.PlaySound(_screamSound);
    }    
}
