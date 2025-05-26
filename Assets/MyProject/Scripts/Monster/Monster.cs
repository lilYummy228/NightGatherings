using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] private MonsterSetup _monster;
    [SerializeField] private Wardrobe _wardrobe;

    private WaitForSeconds _wait;
    private WaitForSeconds _sleep;
    private Coroutine _stateChangerCoroutine = null;
    private Coroutine _bideCoroutine;
    private bool _isChangingState;
    private float _minSleepTime = 2f;
    private float _maxSleepTime = 4f;

    public event Action Jumpedscare;

    private void Awake()
    {
        _wait = new WaitForSeconds(_monster.JumpscareDelay);
        _sleep = new WaitForSeconds(UnityEngine.Random.Range(_minSleepTime, _maxSleepTime));

        _bideCoroutine = StartCoroutine(Bide());
    }

    private void OnEnable() =>
        _wardrobe.OnOpened += TryToJumpscare;

    private void OnDisable() =>
        _wardrobe.OnOpened -= TryToJumpscare;

    public void SetAbleStatus(bool isChangingState) =>
        _isChangingState = isChangingState;

    public void TryToJumpscare() =>
        StartCoroutine(WaitForChangeState());

    private IEnumerator Bide()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_monster.BideMinTime, _monster.BideMaxTime));

        _wardrobe.InteractWithDoor(true);
    }

    private IEnumerator Hide()
    {
        yield return _sleep;

        _wardrobe.InteractWithDoor(false);

        StartCoroutine(Bide());
    }

    private IEnumerator WaitForChangeState()
    {
        yield return _wait;

        if (_isChangingState)
            Jumpscare();
        else
            StartCoroutine(Hide());
    }

    private void Jumpscare()
    {
        StopCoroutine(_bideCoroutine);

        Jumpedscare?.Invoke();

        _soundPlayer.PlaySound(_screamSound);
    }
}
