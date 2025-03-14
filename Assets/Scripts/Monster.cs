using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Wardrobe _wardrobe;

    private float _bideMinTime = 3;
    private float _bideMaxTime = 6;
    private float _spookDelay = 0.35f;
    private bool _isAbleToSpook;
    private Coroutine _coroutine;
    private WaitUntil _waitUntil;
    private WaitForSeconds _waitForSpook;

    public event Action Spooked;

    private void Awake()
    {
        _waitUntil = new WaitUntil(() => _isAbleToSpook == false);
        _waitForSpook = new WaitForSeconds(_spookDelay);

        StartCoroutine(nameof(Bide));
    }

    private void OnEnable() =>
        _wardrobe.DoorOpened += Release;

    private void OnDisable() =>
        _wardrobe.DoorOpened -= Release;

    public void SetSpookStatus(bool isAbleToSpook) =>
        _isAbleToSpook = isAbleToSpook;

    private IEnumerator Bide()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_bideMinTime, _bideMaxTime));

            _wardrobe.InteractWithDoor(_isAbleToSpook);

            yield return _waitUntil;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _wardrobe.InteractWithDoor(false);
        }
    }

    private IEnumerator Spook()
    {
        yield return _waitForSpook;

        Spooked?.Invoke();
    }

    private void Release() =>
        _coroutine = StartCoroutine(Spook());
}
