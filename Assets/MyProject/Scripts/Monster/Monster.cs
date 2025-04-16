using System;
using System.Collections;
using UnityEngine;

public abstract class Monster : MonoBehaviour
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

    public void SetAbleStatus(bool isChangingState) =>
        _isChangingState = isChangingState;

    protected virtual IEnumerator WaitForChangeState(Action action)
    {
        yield return _wait;

        action.Invoke();
    }

    public void TryToJumpscare() => 
        _stateChangerCoroutine = StartCoroutine(WaitForChangeState(Jumpscare));

    protected void Jumpscare()
    {
        StopCoroutine(_bideCoroutine);

        Jumpedscare?.Invoke();

        _soundPlayer.PlaySound(_screamSound);
    }

    public IEnumerator Bide(Action<bool> action)
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
}
