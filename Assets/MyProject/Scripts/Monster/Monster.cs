using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private AudioClip _screamSound;
    [SerializeField] private MonsterSetup _monster;
    [SerializeField] private Wardrobe _wardrobe;

    private WaitUntil _waitUntil;
    private WaitForSeconds _bide;
    private WaitForSeconds _monsterDelay;
    private Coroutine _jumpscareCoroutine;
    private Coroutine _hideCoroutine;
    private Coroutine _bideCoroutine;
    private bool _isChangingState;
    private float _minSleepTime = 2f;
    private float _maxSleepTime = 4f;

    public event Action Jumpedscare;

    private void OnEnable()
    {
        _waitUntil = new WaitUntil(() => _isChangingState);
        _bide = new WaitForSeconds(UnityEngine.Random.Range(_minSleepTime, _maxSleepTime));
        _monsterDelay = new WaitForSeconds(_monster.JumpscareDelay);

        _bideCoroutine = StartCoroutine(Bide());

        _wardrobe.OnOpened += TryToJumpscare;
    }

    private void OnDisable() =>
        _wardrobe.OnOpened -= TryToJumpscare;

    public void SetAbleStatus(bool isChangingState) =>
        _isChangingState = isChangingState;

    public void TryToJumpscare()
    {
        _jumpscareCoroutine = StartCoroutine(JumscareCoroutine());
        _hideCoroutine = StartCoroutine(Hide());
    }

    private IEnumerator Bide()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_monster.BideMinTime, _monster.BideMaxTime));

        _wardrobe.InteractWithDoor(true);
    }

    private IEnumerator Hide()
    {
        yield return _bide;

        if (_jumpscareCoroutine != null)
            StopCoroutine(_jumpscareCoroutine);

        _wardrobe.InteractWithDoor(false);

        _bideCoroutine = StartCoroutine(Bide());
    }

    private IEnumerator JumscareCoroutine()
    {
        yield return _monsterDelay;
        yield return _waitUntil;

        Jumpscare();
    }

    private void Jumpscare()
    {
        StopCoroutine(_hideCoroutine);
        StopCoroutine(_bideCoroutine);

        Jumpedscare?.Invoke();

        _soundPlayer.PlaySound(_screamSound);
    }
}
